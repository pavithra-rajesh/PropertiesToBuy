using System;
using System.Threading.Tasks;
using AutoMapper;
using Funda.Contracts;
using Funda.Services;
using Funda.Services.Mappers;

namespace Funda {
  public class MakelaarService: IMakelaarService {
    #region Dependencies
    private readonly IMakelaarServiceClient _makelaarServiceClient;
    private readonly IMakelaarsResponseMapper _makelaarsResponseMapper;
    private readonly IMapper _mapper;
    #endregion

    public MakelaarService(
      IMakelaarServiceClient makelaarServiceClient,
      IMakelaarsResponseMapper makelaarsResponseMapper,
      IMapper mapper) {
      _makelaarsResponseMapper = makelaarsResponseMapper ?? throw new ArgumentNullException(nameof(MakelaarsResponseMapper));
      _makelaarServiceClient = makelaarServiceClient ?? throw new ArgumentNullException(nameof(makelaarServiceClient));
      _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    /// <summary>
    /// Returns top 10 makelaars in the chosen city with or without tuin based on the parameter 'hasTuin'
    /// </summary>
    public async Task<GetTopMakelaarsResponse> GetTopMakelaarsAsync(string city, bool hasTuin = false) {
      if (string.IsNullOrEmpty(city)) {
        throw new ArgumentNullException(nameof(city));
      }

      var aanbodResponse = await _makelaarServiceClient.GetAanbodAsync(city, hasTuin);
      var topMakelaarsResponse = _makelaarsResponseMapper.ExtractTopMakelaarsFromResponse(aanbodResponse);
      return topMakelaarsResponse;
    }

    /// <summary>
    /// Returns all makelaars in the chosen city 
    /// </summary>
    public async Task<GetAanbodResponse> GetAanbodAsync(string city) {
      if (string.IsNullOrEmpty(city)) {
        throw new ArgumentNullException(nameof(city));
      }

      var aanbodResponse = await _makelaarServiceClient.GetAanbodAsync(city);
      return _makelaarsResponseMapper.MapAanbodResponse(aanbodResponse);
    }
  }
}
