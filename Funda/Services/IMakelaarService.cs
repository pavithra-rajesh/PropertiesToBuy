using System.Threading.Tasks;
using Funda.Contracts;

namespace Funda.Services {
  public interface IMakelaarService {
    Task<GetTopMakelaarsResponse> GetTopMakelaarsAsync(string city, bool hasTuin = false);

    Task<GetAanbodResponse> GetAanbodAsync(string city);
  }
}
