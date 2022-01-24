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
using Server.Extensions;

namespace Server.Services
{
    public class NiceHashDataService : INiceHashDataService
    {
        private readonly ILogger<NiceHashDataService> _logger;
        private readonly HttpClient _client;
        private readonly INiceHashRequestBuilder _niceHashRequestBuilder;

        public NiceHashDataService(HttpClient client, ILogger<NiceHashDataService> logger, INiceHashRequestBuilder niceHashRequestBuilder)
        {
            _client = client;
            _logger = logger;
            _niceHashRequestBuilder = niceHashRequestBuilder;
        }
        
        public async Task<string> GetServerTime(CancellationToken token = default)
        {
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
            var baseUrl = _client?.BaseAddress?.AbsoluteUri;
            if (string.IsNullOrEmpty(baseUrl)) throw new Exception("GetBtcBalance Error: Client is null.");
            
            var request = _niceHashRequestBuilder.GenerateRequest(baseUrl, endpoint, RequestMethod.GET, serverTime);
            request.Method = HttpMethod.Get;

            var content = await _client.GetContentAsync<Rigs2>(request, cancellationToken);

            if (content == null) throw new Exception("Api Error: Content is null.");

            return content;
        }

        public async Task<Currency> GetBtcBalance(string serverTime, CancellationToken cancellationToken = default)
        {
            const string endpoint = "main/api/v2/accounting/accounts2?fiat=GBP&extendedResponse=false";

            var baseUrl = _client?.BaseAddress?.AbsoluteUri;
            if (string.IsNullOrEmpty(baseUrl)) throw new Exception("GetBtcBalance Error: Client is null.");
            
            var request = _niceHashRequestBuilder
                .GenerateRequest(baseUrl, endpoint, RequestMethod.GET, serverTime);
            request.Method = HttpMethod.Get;

            var content = await _client.GetContentAsync<Balances>(request, cancellationToken);

            if (content == null) throw new Exception("Api Error: Content is null.");

            return content.Currencies.First(c => c is {Curr: "BTC"});
        }
    }
}
