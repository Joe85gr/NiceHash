using Library.Models;

namespace Domain
{
    public interface IDataService
    {
        Task<string> GetServerTime(CancellationToken token = default);
        Task<Rigs2> GetRigsDetails(string serverTime, CancellationToken cancellationToken = default);
        Task<Currency> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default);
    }
}