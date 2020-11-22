using System.Threading.Tasks;
using Funda.Contracts;

namespace Funda.Services {
  public interface IMakelaarServiceClient {
    Task<GetAanbodResponse> GetAanbodAsync(string city, bool hasTuin = false);
  }
}
