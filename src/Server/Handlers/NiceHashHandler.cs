using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Server.Builders;

namespace Server.Handlers;

public interface INiceHashHandler
{
    Task<NiceHashData> Handle(CancellationToken cancellationToken);
}

public class NiceHashHandler : INiceHashHandler
{
    private readonly INiceHashDataBuilder _dataBuilder;

    public NiceHashHandler(INiceHashDataBuilder dataBuilder)
    {
        _dataBuilder = dataBuilder;
    }

    public async Task<NiceHashData> Handle(CancellationToken cancellationToken)
    {
        return await _dataBuilder.GetNiceHashData(cancellationToken);
    }
}