using System;
using System.Collections.Generic;
using System.Linq;
using Funda.Contracts;
using Funda.Services.Dto;

namespace Funda.Services.Mapping {
  public class MakelaarsResponseMapper: IMakelaarsResponseMapper {
    public GetTopMakelaarsResponse ExtractTopMakelaarsFromResponse(GetAanbodResponse response) {
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
  }
}
