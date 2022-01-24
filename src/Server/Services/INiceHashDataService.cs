using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace Server.Services
{
    public interface INiceHashDataService
    {
        Task<string> GetServerTime(CancellationToken token = default);
        Task<Rigs2> GetRigsDetails(string serverTime, CancellationToken cancellationToken = default);
        Task<Currency> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default);
    }
}