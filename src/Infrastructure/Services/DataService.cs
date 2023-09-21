using System.Globalization;
using System.Text.Json;
using Domain;
using Domain.Encryption;
using FluentResults;
using Infrastructure.Builders;
using Infrastructure.Extensions;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class DataService : IDataService
{
    private readonly ILogger<DataService> _logger;
    private readonly HttpClient _client;

    public DataService(HttpClient client, ILogger<DataService> logger)
    {
        _client = client;
        _logger = logger;
    }
        
    public async Task<Result<string>> GetServerTime(CancellationToken token = default)
    {
        var contentResult = await _client.GetContentAsync<ServerTime>("api/v2/time", token);

        return contentResult.IsFailed 
            ? Result.Fail(contentResult.Errors) 
            : Result.Ok(contentResult.Value.Value.ToString(CultureInfo.InvariantCulture));
    }
        
    public async Task<Result<Rigs2>> GetRigsDetails(string serverTime, CancellationToken cancellationToken = default)
    {
        const string endpoint = "main/api/v2/mining/rigs2";
            
        return await GetContentResult<Rigs2>(serverTime, endpoint, cancellationToken);
    }

    public async Task<Result<Currency>> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default)
    {
        const string endpoint = "main/api/v2/accounting/accounts2?fiat=GBP&extendedResponse=false";
            
        var balancesResult = await GetContentResult<Balances>(serverTime, endpoint, cancellationToken);

        if (balancesResult.IsFailed) return Result.Fail(balancesResult.Errors);
        
        var currency = balancesResult.Value.Currencies.FirstOrDefault(c => c is {Curr: "BTC"});

        if (currency is null)
        {
            var result = JsonSerializer.Serialize(balancesResult.Value);
            _logger.LogError("{ServiceName} Error: Currency is null: {Result}", nameof(DataService), result) ;
            return Result.Fail("Error: Currency is null.");
        }
        
        return Result.Ok(currency);
    }

    private async Task<Result<T>> GetContentResult<T>(string serverTime, string endpoint, CancellationToken cancellationToken = default)
    {
        var baseUrl = _client.BaseAddress?.AbsoluteUri!;
            
        var method = HttpMethod.Get;
            
        var guid = Guid.NewGuid().ToString();
        var hashStructure = new HashStructure(serverTime, "/" + endpoint, method, guid);

        var request = new RequestBuilder(baseUrl, endpoint)
            .WithHeaders(serverTime, hashStructure)
            .WithMethod(method)
            .Build();
        
        return await _client.GetContentAsync<T>(request, cancellationToken);
    }
}