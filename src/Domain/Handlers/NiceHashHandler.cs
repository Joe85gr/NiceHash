using Library.Models;

namespace Domain.Handlers;

public interface INiceHashHandler
{
    Task<RigsActivity> Handle(CancellationToken cancellationToken);
}

public class NiceHashHandler : INiceHashHandler
{
    private readonly IDataService _dataService;

    public NiceHashHandler(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<RigsActivity> Handle(CancellationToken cancellationToken)
    {
        var serverTime = await _dataService.GetServerTime(cancellationToken);

        var rigsDetails = await _dataService.GetRigsDetails(serverTime, cancellationToken);
        var btcBalance = await _dataService.GetBtcBalance(serverTime, cancellationToken);

        var niceHashData = RigsActivity.Map(btcBalance, rigsDetails);

        return niceHashData;
    }
}