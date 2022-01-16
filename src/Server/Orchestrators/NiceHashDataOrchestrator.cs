using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Server.Mappers;
using Server.Services;

namespace Server.Orchestrators;

public class NiceHashDataOrchestrator : INiceHashDataOrchestrator
{
    private readonly INiceHashDataService _niceHashDataService;

    public NiceHashDataOrchestrator(INiceHashDataService niceHashDataService)
    {
        _niceHashDataService = niceHashDataService;
    }
    
    public async Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default)
    {
        var serverTime = await _niceHashDataService.GetServerTime(cancellationToken);

        if (string.IsNullOrEmpty(serverTime)) return null;

        var rigsDetails = await _niceHashDataService.GetRigsDetails(serverTime, cancellationToken);
        var btcBalance = await _niceHashDataService.GetBtcBalance(serverTime, cancellationToken);

        var niceHashData = Mapper.MapNiceHashDataAsync(btcBalance, rigsDetails);

        return niceHashData;
    }
}