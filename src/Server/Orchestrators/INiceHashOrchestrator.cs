using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace Server.Orchestrators;

public interface INiceHashOrchestrator
{
    Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default);
}