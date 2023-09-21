using FluentResults;
using Library.Models;

namespace Domain.Handlers;

public interface INiceHashHandler
{
    Task<Result<RigsActivity>> Handle(CancellationToken cancellationToken);
}

public class NiceHashHandler : INiceHashHandler
{
    private readonly IDataService _dataService;

    public NiceHashHandler(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<Result<RigsActivity>> Handle(CancellationToken cancellationToken)
    {
        var serverTimeResult = await _dataService.GetServerTime(cancellationToken);

        if (serverTimeResult.IsFailed) return Result.Fail<RigsActivity>(serverTimeResult.Errors);
        
        var btcBalance = await _dataService.GetBtcBalance(serverTimeResult.Value, cancellationToken);
        if(btcBalance.IsFailed) return Result.Fail<RigsActivity>(btcBalance.Errors);

        var rigsDetails = await _dataService.GetRigsDetails(serverTimeResult.Value, cancellationToken);

        if(rigsDetails.IsFailed) return Result.Fail<RigsActivity>(rigsDetails.Errors);
        
        var niceHashData = Result.Ok(RigsActivity.Map(btcBalance.Value, rigsDetails.Value));

        return niceHashData;
    }
}