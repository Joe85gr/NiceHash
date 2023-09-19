using System.Globalization;
using System.Net.Http.Json;
using Domain;
using Domain.Encryption;
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
        
    public async Task<string> GetServerTime(CancellationToken token = default)
    {
        var response = await _client.GetAsync("/api/v2/time", token);

        response.EnsureSuccessStatusCode();

        var serverTime = await response.Content.ReadFromJsonAsync<ServerTime>(cancellationToken: token);
        
        if (serverTime == null) throw new Exception($"{nameof(DataService)} Error: ServerTime is null.");

        return serverTime.Value.ToString(CultureInfo.InvariantCulture);
    }
        
    public async Task<Rigs2> GetRigsDetails(string serverTime, CancellationToken cancellationToken = default)
    {
        const string endpoint = "main/api/v2/mining/rigs2";
            
        var content = await GetContent<Rigs2>(serverTime, endpoint, cancellationToken);

        return content;
    }

    public async Task<Currency> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default)
    {
        const string endpoint = "main/api/v2/accounting/accounts2?fiat=GBP&extendedResponse=false";
            
        var content = await GetContent<Balances>(serverTime, endpoint, cancellationToken);

        if (content?.Currencies is null) throw new Exception($"{nameof(DataService)} Error: Content is null.");
        
        return content.Currencies.FirstOrDefault(c => c is {Curr: "BTC"})!;
    }

    private async Task<T> GetContent<T>(string serverTime, string endpoint, CancellationToken cancellationToken = default)
    {
        var baseUrl = _client.BaseAddress?.AbsoluteUri;
        if (string.IsNullOrEmpty(baseUrl)) throw new Exception("GetBtcBalance Error: Client is null.");
            
        var method = HttpMethod.Get;
            
        var guid = Guid.NewGuid().ToString();
        var hashStructure = new HashStructure(serverTime, "/" + endpoint, method, guid);

        var request = new RequestBuilder()
            .WithUri(baseUrl, endpoint)
            .WithHeaders(serverTime, hashStructure)
            .WithMethod(method)
            .Build();
        
        var content = await _client.GetContentAsync<T>(request, cancellationToken);

        if (content == null) throw new Exception($"{nameof(DataService)} Error: Content is null.");

        return content;
    }
}