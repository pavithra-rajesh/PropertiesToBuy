using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Funda.Contracts;
using Funda.Services;
using Funda.Services.Mapping;
using FundaTest.Builders;
using Moq;
using Xunit;

namespace FundaTest.Services {
  public class MakelaarResponseMapperTests {
    #region Mocks
    private readonly Mock<IMakelaarsResponseMapper> _makelaarsResponseMapperMock;
    private readonly Mock<IMakelaarServiceClient> _makelaarServiceClientMock;
    #endregion Mocks

    private readonly MakelaarService _sut;

    public MakelaarResponseMapperTests() {
      _makelaarsResponseMapperMock = new Mock<IMakelaarsResponseMapper>();
      _makelaarServiceClientMock = new Mock<IMakelaarServiceClient>();
      
      _sut = new MakelaarService(_makelaarServiceClientMock.Object, _makelaarsResponseMapperMock.Object);
    }

    [Fact]
    public void GetTopMakelaarsAsync_WithEmptyCity_ShouldThrowException() {
      // Arrange
      const string city = "";

      // Act
      Func<Task> action = () => _sut.GetTopMakelaarsAsync(city);

      // Assert
      action.Should().ThrowExactly<ArgumentNullException>()
        .And.ParamName.Should().Be("city");
    }

    [Fact]
    public void GetAanbodAsync_WithEmptyCity_ShouldThrowException() {
      // Arrange
      const string city = "";

      // Act
      Func<Task> action = () => _sut.GetAanbodAsync(city);

      // Assert
      action.Should().ThrowExactly<ArgumentNullException>()
        .And.ParamName.Should().Be("city");
    }

    [Fact]
    public void GetTopMakelaarsAsync_WithCity_ShouldReturn_ExpectedResponse() {
      // Arrange
      const string city = "Amsterdam";

      var getTopMakelaarsResponse = new Builder().CreateNew<GetTopMakelaarsResponse>()
        .WithDefaults()
        .Build();

      var getAanbodResponse = new Builder().CreateNew<GetAanbodResponse>()
        .WithDefaults()
        .Build();

      _makelaarServiceClientMock.Setup(m => m.GetAanbodAsync(city, false))
        .ReturnsAsync(getAanbodResponse);

      _makelaarsResponseMapperMock.Setup(m => m.ExtractTopMakelaarsFromResponse(getAanbodResponse))
        .Returns(getTopMakelaarsResponse);

      // Act
      var result = _sut.GetTopMakelaarsAsync(city).Result;

      // Assert
      result.Should().BeEquivalentTo(getTopMakelaarsResponse);
    }

    [Fact]
    public void GetTopMakelaarsAsync_WithCity_AndTuin_ShouldReturn_ExpectedResponse() {
      // Arrange
      const string city = "Amsterdam";

      var getTopMakelaarsResponse = new Builder().CreateNew<GetTopMakelaarsResponse>()
        .WithDefaults(true)
        .Build();

      var getAanbodResponse = new Builder().CreateNew<GetAanbodResponse>()
        .WithDefaults(true)
        .Build();

      _makelaarServiceClientMock.Setup(m => m.GetAanbodAsync(city, true))
        .ReturnsAsync(getAanbodResponse)
        .Verifiable();

      _makelaarsResponseMapperMock.Setup(m => m.ExtractTopMakelaarsFromResponse(getAanbodResponse))
        .Returns(getTopMakelaarsResponse)
        .Verifiable();

      // Act
      var result = _sut.GetTopMakelaarsAsync(city, true).Result;

      // Assert
      result.Should().BeEquivalentTo(getTopMakelaarsResponse);
      _makelaarServiceClientMock.Verify();
      _makelaarsResponseMapperMock.Verify();
    }

    [Fact]
    public void GetAanbodAsync_WithCity_ShouldReturn_ExpectedResponse() {
      // Arrange
      const string city = "Amsterdam";

      var getAanbodResponse = new Builder().CreateNew<GetAanbodResponse>()
        .WithDefaults()
        .Build();

      _makelaarServiceClientMock.Setup(m => m.GetAanbodAsync(city, false))
        .ReturnsAsync(getAanbodResponse)
        .Verifiable();

      // Act
      var result = _sut.GetAanbodAsync(city).Result;

      // Assert
      result.Should().BeEquivalentTo(getAanbodResponse);
      _makelaarServiceClientMock.Verify();
    }
  }
}
