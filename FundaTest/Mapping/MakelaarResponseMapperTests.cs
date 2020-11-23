using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Funda.Contracts;
using Funda.Services.Dto;
using Funda.Services.Mappers;
using FundaTest.Builders;
using FundaTest.Builders.Contracts;
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
      const AanbodResponse response = null;

      // Act
      Action action = () => _sut.ExtractTopMakelaarsFromResponse(response);

      // Assert
      action.Should().ThrowExactly<ArgumentNullException>()
        .And.ParamName.Should().Be("response");
    }

    [Fact]
    public void ExtractTopMakelaarsFromResponse_WithAanbodResponseResponse_ShouldReturnExpectedResponse() {
      // Arrange
      var aanbodResponse = new Builder().CreateNew<AanbodResponse>()
        .WithDefaults()
        .Build();

      var getTopMakelaarsResponse = new Builder().CreateNew<GetTopMakelaarsResponse>()
        .WithDefaults()
        .Build();

      // Act
      var result = _sut.ExtractTopMakelaarsFromResponse(aanbodResponse);

      // Assert
      result.Should().BeEquivalentTo(getTopMakelaarsResponse);
    }

    [Fact]
    public void MapAanbodResponse_WithAanbodResponseResponse_ShouldReturnExpectedResponse() {
      // Arrange
      var aanbodResponse = new Builder().CreateNew<AanbodResponse>()
        .WithDefaults()
        .Build();

      var getAanbodResponse = new Builder().CreateNew<GetAanbodResponse>()
        .WithDefaults()
        .Build();

      // Act
      var result = _sut.MapAanbodResponse(aanbodResponse);

      // Assert
      result.Should().BeEquivalentTo(getAanbodResponse);
    }
  }
}
