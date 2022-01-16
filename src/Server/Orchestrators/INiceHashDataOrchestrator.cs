using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace Server.Orchestrators;

public interface INiceHashDataOrchestrator
{
    Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default);
}