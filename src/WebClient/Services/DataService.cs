using System;
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
        private readonly HttpClient _client;

        public DataService(HttpClient client, ILogger<DataService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<NiceHashData> GetNiceHashAsync(CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAsync("api/NiceHash", cancellationToken);

            if(response.IsSuccessStatusCode == false)
            {
                _logger.LogError($"GetNiceHashAsync error: Could not retrieve NiceHash data.");
                return null;
            }
                
            var content = await response.Content.ReadFromJsonAsync<NiceHashData>(cancellationToken: cancellationToken);

            return content;
        }
    }
}