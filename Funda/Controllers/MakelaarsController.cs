using System;
using System.Threading.Tasks;
using Funda;
using Funda.Contracts;
using Funda.Services;
using Microsoft.AspNetCore.Mvc;

namespace funda.Controllers {
  [Route("api/[controller]")]
  public class MakelaarsController : Controller {
    private readonly IMakelaarService _makelaarService;

    public MakelaarsController(IMakelaarService makelaarService) {
      _makelaarService = makelaarService ?? throw new ArgumentNullException(nameof(makelaarService));
    }

    [HttpGet("[action]/{city}/{hasTuin}")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "city", "hasTuin" })]
    public async Task<GetTopMakelaarsResponse> GetTopMakelaars(string city, bool hasTuin = false) {
      return await _makelaarService.GetTopMakelaarsAsync(city, hasTuin);
    }

    [HttpGet("[action]/{city}")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "city" })]
    public async Task<GetAanbodResponse> GetAllMakelaars(string city) {
      return await _makelaarService.GetAanbodAsync(city);
    }
  }
}