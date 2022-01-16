using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using MediatR;
using Server.Orchestrators;
using Server.Queries;

namespace Server.Handlers;

public class NiceHashHandler : IRequestHandler<NiceHashQuery, NiceHashData>
{
    private readonly INiceHashDataOrchestrator _dataOrchestrator;

    public NiceHashHandler(INiceHashDataOrchestrator dataOrchestrator)
    {
        _dataOrchestrator = dataOrchestrator;
    }

    public async Task<NiceHashData> Handle(NiceHashQuery request, CancellationToken cancellationToken)
    {
        return await _dataOrchestrator.GetNiceHashData(cancellationToken);
    }
}