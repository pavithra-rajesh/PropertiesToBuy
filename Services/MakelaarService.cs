using System;
using System.Threading.Tasks;
using Funda.Contracts;
using Funda.Services.Mapping;

namespace Funda.Services {
  public class MakelaarService: IMakelaarService {
    private readonly IMakelaarServiceClient _makelaarServiceClient;
    private readonly IMakelaarsResponseMapper _makelaarsResponseMapper;

    public MakelaarService(
      IMakelaarServiceClient makelaarServiceClient,
      IMakelaarsResponseMapper makelaarsResponseMapper) {
      _makelaarsResponseMapper = makelaarsResponseMapper ?? throw new ArgumentNullException(nameof(MakelaarsResponseMapper));
      _makelaarServiceClient = makelaarServiceClient ?? throw new ArgumentNullException(nameof(makelaarServiceClient));
    }

    public async Task<GetTopMakelaarsResponse> GetTopMakelaarsAsync(string city, bool hasTuin = false) {
      if (string.IsNullOrEmpty(city)) {
        throw new ArgumentNullException(nameof(city));
      }

      var aanbodResponse = await _makelaarServiceClient.GetAanbodAsync(city, hasTuin);
      var topMakelaarsResponse = _makelaarsResponseMapper.ExtractTopMakelaarsFromResponse(aanbodResponse);
      return topMakelaarsResponse;
    }

    public async Task<GetAanbodResponse> GetAanbodAsync(string city) {
      if (string.IsNullOrEmpty(city)) {
        throw new ArgumentNullException(nameof(city));
      }

      return await _makelaarServiceClient.GetAanbodAsync(city);
    }
  }
}
