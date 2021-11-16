using Library.Encryption;
using Library.Mappers;
using Library.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Library.Services
{
    public class NiceHashService : INiceHashService
    {
        private HttpClient Client;
        private string ServerTime = "";

        public NiceHashService(HttpClient client)
        {
            Client = client;    
        }

        public async Task<NiceHashData> GetAll(CancellationToken token = default)
        {
            ServerTime = await GetServerTime();

            var rigsDetails = await GetRigsDetails();
            var btcBalance = await GetBtcBalance();

            var niceHashData = Mapper.MapNiceHashDataAsync(btcBalance, rigsDetails);

            return niceHashData;
        }
        private async Task<string> GetServerTime(CancellationToken token = default)
        {
            var serverTime = await Client.GetFromJsonAsync<ServerTime>($"/api/v2/time", token);

            if (serverTime is null) throw new Exception("Server Time Api Error.");

            return serverTime.Value.ToString();
        }
        private async Task<Rigs2> GetRigsDetails(CancellationToken token = default)
        {
            var endpoint = "main/api/v2/mining/rigs2";

            var request = SetNiceHashRequestWithCredentials(endpoint, RequestMethod.GET);
            request.Method = HttpMethod.Get;

            var response = await Client.SendAsync(request, token);
            response.EnsureSuccessStatusCode();
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
            response.EnsureSuccessStatusCode();
            var responseStream = response.Content.ReadAsStream();
            var content = await JsonSerializer.DeserializeAsync<Balances>(responseStream, cancellationToken: token);

            if (content == null) throw new Exception("Api Error: Content is null.");

            return content.Currencies.First(c => c.Curr == "BTC");
        }

        private HttpRequestMessage SetNiceHashRequestWithCredentials(string endpoint, RequestMethod method)
        {
            if (Client is null || Client.BaseAddress is null) throw new Exception("");

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
