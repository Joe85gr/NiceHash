using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Library.Models;

namespace WebClient.Services;

public interface IServerData
{
    Task<Result<RigsActivity>> GetNiceHashAsync(CancellationToken cancellationToken = default);
}