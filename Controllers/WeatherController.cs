using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherChecker.Controllers {
  [Route("api/[controller]")]
  public class WeatherController : Controller {
    [HttpGet("[action]/{city}")]
    public async Task<IActionResult> City(string city) {
      using (var client = new HttpClient()) {
        try {
          client.BaseAddress = new Uri("http://api.openweathermap.org");
          var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=22dfa2dfa36a43347ce187f7117e4b52&units=metric");
          response.EnsureSuccessStatusCode();

          var stringResult = await response.Content.ReadAsStringAsync();
          var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
          return Ok(new {
            Temp = rawWeather.Main.Temp,
            Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
            City = rawWeather.Name
          });
        }
        catch (HttpRequestException httpRequestException) {
          return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
        }
      }
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