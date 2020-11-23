using System.Threading.Tasks;
using Funda.Services.Dto;

namespace Funda.Services {
  public interface IMakelaarServiceClient {
    Task<AanbodResponse> GetAanbodAsync(string city, bool hasTuin = false);
  }
}
