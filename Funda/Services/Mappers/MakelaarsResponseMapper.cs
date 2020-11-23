using System;
using System.Collections.Generic;
using System.Linq;
using Funda.Contracts;
using Funda.Services.Dto;
using TopMakelaar = Funda.Contracts.TopMakelaar;

namespace Funda.Services.Mappers {
  public class MakelaarsResponseMapper: IMakelaarsResponseMapper {
    public GetTopMakelaarsResponse ExtractTopMakelaarsFromResponse(AanbodResponse response) {
      if (response == null) {
        throw new ArgumentNullException(nameof(response));
      }

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

    // TODO: Automapper is a simpler solution instead, tried using it but kept throwing unsupported mapping exception, so used this as a temp. solution
    public GetAanbodResponse MapAanbodResponse(AanbodResponse response) {
      if (response == null) {
        throw new ArgumentNullException(nameof(response));
      }
      GetAanbodResponse getAanbodResponse = new GetAanbodResponse();
      int numObjects = response.objects.Length;
      getAanbodResponse.objects = new Contracts.Object[numObjects];
      int i = 0;
      foreach (var responseObject in response.objects) {
        var aanbodObject = new Contracts.Object();
        aanbodObject.MakelaarId = responseObject.MakelaarId;
        aanbodObject.MakelaarNaam = responseObject.MakelaarNaam;
        aanbodObject.Woonplaats = responseObject.Woonplaats;
        getAanbodResponse.objects[i] = aanbodObject;
        i++;
      }

      return getAanbodResponse;
    }
  }
}
