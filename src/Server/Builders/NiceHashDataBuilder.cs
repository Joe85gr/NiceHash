using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Server.Services;

namespace Server.Builders;

public class NiceHashDataBuilder : INiceHashDataBuilder
{
    private readonly INiceHashDataService _niceHashDataService;

    public NiceHashDataBuilder(INiceHashDataService niceHashDataService)
    {
        _niceHashDataService = niceHashDataService;
    }
    
    public async Task<NiceHashData> GetNiceHashData(CancellationToken cancellationToken = default)
    {
        var serverTime = await _niceHashDataService.GetServerTime(cancellationToken);

        if (string.IsNullOrEmpty(serverTime)) return null;

        var rigsDetails = await _niceHashDataService.GetRigsDetails(serverTime, cancellationToken);
        var btcBalance = await _niceHashDataService.GetBtcBalance(serverTime, cancellationToken);

        var niceHashData = NiceHashData.Map(btcBalance, rigsDetails);

        return niceHashData;
    }
}