using FluentResults;
using Library.Models;

namespace Domain
{
    public interface IDataService
    {
        Task<Result<string>> GetServerTime(CancellationToken token = default);
        Task<Result<Rigs2>> GetRigsDetails(string serverTime, CancellationToken cancellationToken = default);
        Task<Result<Currency>> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default);
    }
}