using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using MediatR;
using Server.Builders;
using Server.Queries;

namespace Server.Handlers;

public class NiceHashHandler : IRequestHandler<NiceHashQuery, NiceHashData>
{
    private readonly INiceHashDataBuilder _dataBuilder;

    public NiceHashHandler(INiceHashDataBuilder dataBuilder)
    {
        _dataBuilder = dataBuilder;
    }

    public async Task<NiceHashData> Handle(NiceHashQuery request, CancellationToken cancellationToken)
    {
        return await _dataBuilder.GetNiceHashData(cancellationToken);
    }
}