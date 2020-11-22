using System;

namespace Funda.Configuration {
  public interface IFundaConfiguration {
    Uri BaseUri { get; }
    string ApiKey { get; }
  }
}
