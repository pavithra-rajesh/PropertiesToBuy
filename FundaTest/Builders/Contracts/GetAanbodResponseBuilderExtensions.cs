using System;
using System.Linq;
using FizzWare.NBuilder;
using Funda.Contracts;
using Object = Funda.Contracts.Object;

namespace FundaTest.Builders.Contracts {
  internal static class GetAanbodResponseResponseBuilderExtensions { 
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
