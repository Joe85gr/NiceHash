using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Server.Mappers;
using Server.Services;

namespace Server.Orchestrators;

public class NiceHashOrchestrator : INiceHashOrchestrator
{
    private readonly INiceHashService _niceHashService;

    public NiceHashOrchestrator(INiceHashService niceHashService)
    {
        _niceHashService = niceHashService;
    }
    
    public async Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default)
    {
        var serverTime = await _niceHashService.GetServerTime(cancellationToken);

        if (string.IsNullOrEmpty(serverTime)) return null;

        var rigsDetails = await _niceHashService.GetRigsDetails(serverTime, cancellationToken);
        var btcBalance = await _niceHashService.GetBtcBalance(serverTime, cancellationToken);

        var niceHashData = Mapper.MapNiceHashDataAsync(btcBalance, rigsDetails);

        return niceHashData;
    }
}