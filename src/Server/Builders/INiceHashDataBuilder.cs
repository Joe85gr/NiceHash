using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace Server.Builders;

public interface INiceHashDataBuilder
{
    Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default);
}