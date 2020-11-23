using Funda.Contracts;
using Funda.Services.Dto;

namespace Funda.Services.Mappers {
  public interface IMakelaarsResponseMapper {
    GetTopMakelaarsResponse ExtractTopMakelaarsFromResponse(AanbodResponse response);
    GetAanbodResponse MapAanbodResponse(AanbodResponse response);
  }
}
