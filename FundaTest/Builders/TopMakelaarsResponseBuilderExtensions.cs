using System;
using System.Linq;
using FizzWare.NBuilder;
using Funda.Contracts;
using Funda.Services.Dto;
using Object = Funda.Contracts.Object;

namespace FundaTest.Builders {
  internal static class TopMakelaarsResponseBuilderExtensions {
    public static ISingleObjectBuilder<GetTopMakelaarsResponse> WithDefaults(
      this ISingleObjectBuilder<GetTopMakelaarsResponse> getTopMakelaarResponseBuilder, bool hasTuin = false) {
      if (getTopMakelaarResponseBuilder == null) {
        throw new ArgumentNullException(nameof(getTopMakelaarResponseBuilder));
      }

      var topMakelaars = new Builder().CreateListOfSize<TopMakelaar>(1).WithDefaults().Build().ToList();
      return getTopMakelaarResponseBuilder
        .With(x => x.topMakelaars = topMakelaars);
    }

    public static IListBuilder<TopMakelaar> WithDefaults(
      this IListBuilder<TopMakelaar> topMakelaarBuilder, bool hasTuin = false) {
      if (topMakelaarBuilder == null) {
        throw new ArgumentNullException(nameof(topMakelaarBuilder));
      }

      return topMakelaarBuilder
        .All()
        .And(x => x.MakelaarId = hasTuin ? MakelaarsTestDefaults.MakelaarIdWithTuin : MakelaarsTestDefaults.MakelaarId)
        .And(x => x.Count = MakelaarsTestDefaults.Count)
        .And(x => x.MakelaarName = hasTuin ? MakelaarsTestDefaults.MakelaarNaamWithTuin : MakelaarsTestDefaults.MakelaarNaam);
    }

    public static ISingleObjectBuilder<GetAanbodResponse> WithDefaults(
      this ISingleObjectBuilder<GetAanbodResponse> getAanbodResponseBuilder, bool hasTuin = false) {
      if (getAanbodResponseBuilder == null) {
        throw new ArgumentNullException(nameof(getAanbodResponseBuilder));
      }

      var objects = new Builder().CreateListOfSize<Object>(5).WithDefaults(hasTuin).Build().ToArray();
      return getAanbodResponseBuilder
        .With(x => x.objects = objects);
    }

    public static IListBuilder<Object> WithDefaults(
      this IListBuilder<Object> objectBuilder, bool hasTuin = false) {
      if (objectBuilder == null) {
        throw new ArgumentNullException(nameof(objectBuilder));
      }

      return objectBuilder
        .All()
        .And(x => x.MakelaarId = hasTuin ? MakelaarsTestDefaults.MakelaarIdWithTuin : MakelaarsTestDefaults.MakelaarId)
        .And(x => x.Woonplaats = MakelaarsTestDefaults.Woonplaats)
        .And(x => x.MakelaarNaam = hasTuin ? MakelaarsTestDefaults.MakelaarNaamWithTuin : MakelaarsTestDefaults.MakelaarNaam);
    }
  }
}
