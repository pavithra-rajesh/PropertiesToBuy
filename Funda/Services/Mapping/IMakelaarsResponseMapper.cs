using Funda.Contracts;

namespace Funda.Services.Mapping {
  public interface IMakelaarsResponseMapper {
    GetTopMakelaarsResponse ExtractTopMakelaarsFromResponse(GetAanbodResponse response);
  }
}
