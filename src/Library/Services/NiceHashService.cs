using Library.Encryption;
using Library.Mappers;
using Library.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Library.Services
{
    public class NiceHashService : INiceHashService
    {
        private readonly ILogger<NiceHashService> _logger;
        private HttpClient Client;
        private string ServerTime = "";

        public NiceHashService(HttpClient client, ILogger<NiceHashService> logger)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<NiceHashData> GetAll(CancellationToken token = default)
        {
            ServerTime = await GetServerTime();

            if (string.IsNullOrEmpty(ServerTime)) return null;

            var rigsDetails = await GetRigsDetails();
            var btcBalance = await GetBtcBalance();

            if(btcBalance is null || rigsDetails is null) return null;

            var niceHashData = Mapper.MapNiceHashDataAsync(btcBalance, rigsDetails);

            return niceHashData;
        }
        private async Task<string> GetServerTime(CancellationToken token = default)
        {
            var response = await Client.GetAsync("/api/v2/time", token);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogCritical($"GetServerTime error: Could not retrieve ServerTime data.");
                return null;
            }

            var serverTime = await response.Content.ReadFromJsonAsync<ServerTime>(cancellationToken: token);

            return serverTime.Value.ToString();

        }
        private async Task<Rigs2> GetRigsDetails(CancellationToken token = default)
        {
            var endpoint = "main/api/v2/mining/rigs2";

            var request = SetNiceHashRequestWithCredentials(endpoint, RequestMethod.GET);
            request.Method = HttpMethod.Get;

            var response = await Client.SendAsync(request, token);

            _logger.LogInformation("Hello there :D");

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"GetRigsDetails error: {ex.Message} ", ex);
                return null;
            }

            var responseStream = response.Content.ReadAsStream();
            var content = await JsonSerializer.DeserializeAsync<Rigs2>(responseStream, cancellationToken: token);

            if (content == null) throw new Exception("Api Error: Content is null.");

            return content;
        }

        private async Task<Currency> GetBtcBalance(CancellationToken token = default)
        {
            var endpoint = "main/api/v2/accounting/accounts2?fiat=GBP&extendedResponse=false";

            var request = SetNiceHashRequestWithCredentials(endpoint, RequestMethod.GET);
            request.Method = HttpMethod.Get;

            var response = await Client.SendAsync(request, token);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"GetRigsDetails error: {ex.Message}", ex);
                return null;
            }

            var responseStream = response.Content.ReadAsStream();
            var content = await JsonSerializer.DeserializeAsync<Balances>(responseStream, cancellationToken: token);

            if (content == null) throw new Exception("Api Error: Content is null.");

            return content.Currencies.First(c => c.Curr == "BTC");
        }

        private HttpRequestMessage SetNiceHashRequestWithCredentials(string endpoint, RequestMethod method)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(Client.BaseAddress.AbsoluteUri + endpoint)
            };

            var hashStructure = new HashStructure(ServerTime, "/" + endpoint, method);
            var encryptedHash = Sha256Encryption.GenerateEncryptedHash(hashStructure);

            request.Headers.Add("X-Time", ServerTime);
            request.Headers.Add("X-Nonce", hashStructure.Nonce);
            request.Headers.Add("X-Auth", hashStructure.ApiKey + ":" + encryptedHash);
            request.Headers.Add("X-Organization-Id", hashStructure.OrgId);

            return request;
        }
    }
}
