using System;
using System.Net.Http;
using System.Threading.Tasks;
using Funda.Configuration;
using Funda.Contracts;
using Newtonsoft.Json;

namespace Funda.Services {
  public class MakelaarServiceClient : IMakelaarServiceClient {
    private const string _resource = "feeds/Aanbod.svc/json/";
    private readonly string _resourceWithApiKey;

    #region Dependencies
    private readonly IFundaConfiguration _fundaConfiguration;
    #endregion

    public MakelaarServiceClient(IFundaConfiguration fundaConfiguration) {
      _fundaConfiguration = fundaConfiguration ?? throw new ArgumentNullException(nameof(fundaConfiguration));

      _resourceWithApiKey = _resource + _fundaConfiguration.ApiKey;
    }

    public async Task<GetAanbodResponse> GetAanbodAsync(string city, bool hasTuin = false) {
      if (string.IsNullOrEmpty(city)) {
        throw new ArgumentNullException(nameof(city));
      }

      using (var client = new HttpClient()) {
        try {
          client.BaseAddress = _fundaConfiguration.BaseUri;
          var requestUri = getRequestUri(city, hasTuin);
          var response = await client.GetAsync(requestUri);
          response.EnsureSuccessStatusCode();

          var stringResult = await response.Content.ReadAsStringAsync();
          var aanbodResponse = JsonConvert.DeserializeObject<GetAanbodResponse>(stringResult);
          return aanbodResponse;
        }
        catch (HttpRequestException httpRequestException) {
          throw new HttpRequestException($"Error getting response from Aanbod service: {httpRequestException.Message}");
        }
      }
    }

    private string getRequestUri(string city, bool hasTuin) {
      var requestUriWithcity = _resourceWithApiKey + "/?type=koop&zo=/" + city;
      return hasTuin ? (requestUriWithcity + "/tuin") : requestUriWithcity;
    }
  }
}
