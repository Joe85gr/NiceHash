using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;

namespace WebClient.Services
{
    public class DataService : IDataService
    {
        private HttpClient Client;

        public DataService(HttpClient client)
        {
            Client = client;
        }

        public async Task<NiceHashData> GetNiceHashAsync(CancellationToken token = default)
        {
            var content = await Client.GetFromJsonAsync<NiceHashData>($"api/NiceHash", token);

            return content;
        }
    }
}