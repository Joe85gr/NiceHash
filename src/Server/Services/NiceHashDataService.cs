using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.Extensions.Logging;
using Server.Builders;
using Server.Encryption;
using Server.Extensions;

namespace Server.Services;

public class NiceHashDataService : INiceHashDataService
{
    private readonly ILogger<NiceHashDataService> _logger;
    private readonly HttpClient _client;

    public NiceHashDataService(HttpClient client, ILogger<NiceHashDataService> logger)
    {
        _client = client;
        _logger = logger;
    }
        
    public async Task<string> GetServerTime(CancellationToken token = default)
    {
        _logger.LogCritical("GetServerTime: Retrieving ServerTime data");
        var response = await _client.GetAsync("/api/v2/time", token);

        if (response.IsSuccessStatusCode == false)
        {
            _logger.LogCritical($"GetServerTime error: Could not retrieve ServerTime data.");
            return null;
        }

        var serverTime = await response.Content.ReadFromJsonAsync<ServerTime>(cancellationToken: token);

        return serverTime?.Value.ToString(CultureInfo.InvariantCulture);
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

        return content.Currencies.First(c => c is {Curr: "BTC"});
    }

    private async Task<T> GetContent<T>(string serverTime, string endpoint, CancellationToken cancellationToken = default)
    {
        var baseUrl = _client?.BaseAddress?.AbsoluteUri;
        if (string.IsNullOrEmpty(baseUrl)) throw new Exception("GetBtcBalance Error: Client is null.");
            
        var method = HttpMethod.Get;
            
        var guid = Guid.NewGuid().ToString();
        var hashStructure = new HashStructure(serverTime, "/" + endpoint, method, guid);

        var request = new NiceHashRequestBuilder()
            .WithUri(baseUrl, endpoint)
            .WithHeaders(serverTime, hashStructure)
            .WithMethod(method)
            .Build();
        
        var content = await _client.GetContentAsync<T>(request, cancellationToken);

        if (content == null) throw new Exception($"{nameof(NiceHashDataService)} Error: Content is null.");

        return content;
    }
}