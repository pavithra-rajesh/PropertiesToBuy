using System;
using System.Linq;
using FizzWare.NBuilder;
using Funda.Services.Dto;
using Object = Funda.Services.Dto.Object;

namespace FundaTest.Builders {
  internal static class AanbodResponseResponseBuilderExtensions {
    public static ISingleObjectBuilder<AanbodResponse> WithDefaults(
      this ISingleObjectBuilder<AanbodResponse> aanbodResponseBuilder, bool hasTuin = false) {
      if (aanbodResponseBuilder == null) {
        throw new ArgumentNullException(nameof(aanbodResponseBuilder));
      }

      var objects = new Builder().CreateListOfSize<Object>(5).WithDefaults(hasTuin).Build().ToArray();
      return aanbodResponseBuilder
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
