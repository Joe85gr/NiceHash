#nullable enable
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace Server.Services
{
    public interface INiceHashService
    {
        Task<string?> GetServerTime(CancellationToken token = default);
        Task<Rigs2?> GetRigsDetails(string serverTime, CancellationToken token = default);
        Task<Currency?> GetBtcBalance(string serverTime, CancellationToken token = default);
    }
}