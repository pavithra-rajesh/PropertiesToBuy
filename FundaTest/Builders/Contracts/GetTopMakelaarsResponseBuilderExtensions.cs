using System;
using System.Linq;
using FizzWare.NBuilder;
using Funda.Contracts;
using TopMakelaar = Funda.Contracts.TopMakelaar;

namespace FundaTest.Builders.Contracts {
  internal static class GetTopMakelaarsResponseBuilderExtensions {
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
  }
}
