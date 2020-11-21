using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using test;

namespace funda.Controllers {
  [Route("api/[controller]")]
  public class MakelaarsController : Controller {
    [HttpGet("[action]/{city}")]
    public async Task<GetTopMakelaarsResponse> GetTopMakelaars(string city) {
      var aanbodResponse = await GetAanbod(city);
      var topMakelaarsResponse = ExtractTopMakelaarsFromResponse(aanbodResponse);
      return topMakelaarsResponse;
    }

    [HttpGet("[action]/{city}")]
    public async Task<GetTopMakelaarsResponse> GetTopMakelaarsWithTuin(string city) {
      var aanbodResponse = await GetAanbodWithTuin(city);
      var makelaarsWithTuinResponse = ExtractTopMakelaarsFromResponse(aanbodResponse);
      return makelaarsWithTuinResponse;

    }

    [HttpGet("[action]/{city}")]
    public async Task<GetAanbodResponse> GetAllMakelaars(string city) {
      var aanbodResponse = await GetAanbod(city);
      return aanbodResponse;
    }

    private async Task<GetAanbodResponse> GetAanbod(string city) {
      using (var client = new HttpClient()) {
        try {
          client.BaseAddress = new Uri("http://partnerapi.funda.nl/");
          var response = await client.GetAsync($"feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/{city}");
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

    private async Task<GetAanbodResponse> GetAanbodWithTuin(string city) {
      using (var client = new HttpClient()) {
        try {
          client.BaseAddress = new Uri("http://partnerapi.funda.nl/");
          var response = await client.GetAsync($"feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/{city}/tuin");
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

    private GetTopMakelaarsResponse ExtractTopMakelaarsFromResponse(GetAanbodResponse response) {
      var topMakelaarsList = new List<TopMakelaar>();

      var aanbodObjects = response.objects.ToArray();
      var aanbodGroups = aanbodObjects.GroupBy(x => x.MakelaarId)
        .Select(group => new {
          MakelaarId = group.Key,
          Count = group.Count(),
          MakelaarNaam = group.First().MakelaarNaam
        })
        .OrderByDescending(x => x.Count);

      foreach (var aanbodObject in aanbodGroups) {
        var topMakelaar = new TopMakelaar();
        topMakelaar.MakelaarId = aanbodObject.MakelaarId;
        topMakelaar.Count = aanbodObject.Count;
        topMakelaar.MakelaarName = aanbodObject.MakelaarNaam;
        topMakelaarsList.Add(topMakelaar);
      }

      var getTopmakelaarsResponse = new GetTopMakelaarsResponse();
      getTopmakelaarsResponse.topMakelaars = topMakelaarsList;

      return getTopmakelaarsResponse;
    }



    /*[HttpGet("[action]/{city}")]
    public async Task<IRestResponse> City(string city) {

      var restClient = new RestClient();
      restClient.BaseUrl = new Uri("http://partnerapi.funda.nl/");
      var request = new RestRequest(Method.POST);

      // Resource
      request.Resource = string.Format("feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam");
     *//* request.AddParameter("type", "koop");
      request.AddParameter("zo", city);*//*

      try {
        IRestResponse response = await restClient.ExecuteAsync<GetAanbodResponse>(request);
        Console.WriteLine(response.StatusCode);
        return response;
      }
      catch (HttpRequestException httpRequestException) {
        throw new HttpRequestException($"Error getting weather from OpenWeather: {httpRequestException.Message}");
      }
    }*/
  }
}