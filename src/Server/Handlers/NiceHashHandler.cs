using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using MediatR;
using Server.Orchestrators;
using Server.Queries;

namespace Server.Handlers;

public class NiceHashHandler : IRequestHandler<NiceHashQuery, NiceHashData>
{
    private readonly INiceHashOrchestrator _orchestrator;

    public NiceHashHandler(INiceHashOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public async Task<NiceHashData> Handle(NiceHashQuery request, CancellationToken cancellationToken)
    {
        return await _orchestrator.GetNiceHashData(cancellationToken);
    }
}