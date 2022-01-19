using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace WebClient.Services;

public interface IDataService
{
    Task<NiceHashData> GetNiceHashAsync(CancellationToken cancellationToken = default);
}