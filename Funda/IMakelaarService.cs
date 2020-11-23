using System.Threading.Tasks;
using Funda.Contracts;

namespace Funda {
  public interface IMakelaarService {
    Task<GetTopMakelaarsResponse> GetTopMakelaarsAsync(string city, bool hasTuin = false);

    Task<GetAanbodResponse> GetAanbodAsync(string city);
  }
}
