using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace WebClient.Services;

public class ServerData : IServerData
{
    private readonly ILogger<ServerData> _logger;
    private readonly HttpClient _client;

    public ServerData(HttpClient client, ILogger<ServerData> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<RigsActivity> GetNiceHashAsync(CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync("api/Activity", cancellationToken);

        if(response.IsSuccessStatusCode == false)
        {
            _logger.LogError("GetNiceHashAsync error: Could not retrieve NiceHash data");
            return null;
        }
                
        var content = await response.Content.ReadFromJsonAsync<RigsActivity>(cancellationToken: cancellationToken);

        return content;
    }
}