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

namespace FundaTest.Mapping {
  public class MakelaarResponseMapperTests {

    private readonly MakelaarsResponseMapper _sut;

    public MakelaarResponseMapperTests() {
      _sut = new MakelaarsResponseMapper();
    }

    [Fact]
    public void ExtractTopMakelaarsFromResponse_WithEmptyResponse_ShouldThrowException() {
      // Arrange
      const GetAanbodResponse response = null;

      // Act
      Action action = () => _sut.ExtractTopMakelaarsFromResponse(response);

      // Assert
      action.Should().ThrowExactly<ArgumentNullException>()
        .And.ParamName.Should().Be("response");
    }

    [Fact]
    public void ExtractTopMakelaarsFromResponse_WithGetAanbodResponseResponse_ShouldReturnExpectedResponse() {
      // Arrange
      var getAanbodResponse = new Builder().CreateNew<GetAanbodResponse>()
        .WithDefaults()
        .Build();

      var getTopMakelaarsResponse = new Builder().CreateNew<GetTopMakelaarsResponse>()
        .WithDefaults()
        .Build();

      // Act
      var result = _sut.ExtractTopMakelaarsFromResponse(getAanbodResponse);

      // Assert
      result.Should().BeEquivalentTo(getTopMakelaarsResponse);
    }
  }
}
