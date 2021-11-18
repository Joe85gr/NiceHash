using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace WebClient.Services
{
    
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private HttpClient Client;

        public DataService(HttpClient client, ILogger<DataService> logger)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<NiceHashData> GetNiceHashAsync(CancellationToken token = default)
        {
            var response = await Client.GetAsync("api/NiceHash", token);

            if(response.IsSuccessStatusCode == false)
            {
                _logger.LogCritical($"GetNiceHashAsync error: Could not retrieve nicehash data.");
                return null;
            }
                
            var content = await response.Content.ReadFromJsonAsync<NiceHashData>(cancellationToken: token);

            return content;
        }
    }
}