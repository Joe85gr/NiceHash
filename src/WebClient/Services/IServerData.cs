using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace WebClient.Services;

public interface IServerData
{
    Task<RigsActivity> GetNiceHashAsync(CancellationToken cancellationToken = default);
}