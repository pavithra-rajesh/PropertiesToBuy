using System;

namespace Funda.Configuration {
  public class FundaConfiguration: IFundaConfiguration {
    public Uri BaseUri => new Uri("http://partnerapi.funda.nl/");
    public string ApiKey => "ac1b0b1572524640a0ecc54de453ea9f";
  }
}
