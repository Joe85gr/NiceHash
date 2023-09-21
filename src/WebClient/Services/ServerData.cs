using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace WebClient.Services;

public class ServerData : IServerData
{
    private readonly ILogger<ServerData> _logger;
    private readonly HttpClient _client;
    private const string ErrorMessage = "Could not retrieve NiceHash data.";

    public ServerData(HttpClient client, ILogger<ServerData> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<Result<RigsActivity>> GetNiceHashAsync(CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync("api/Activity", cancellationToken);

        if(response.IsSuccessStatusCode == false)
        {
            return await ErrorResult<RigsActivity>(response, cancellationToken);
        }
                
        var content = await response.Content.ReadFromJsonAsync<RigsActivity>(cancellationToken: cancellationToken);

        return Result.Ok(content);
    }

    private async Task<Result<T>> ErrorResult<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        _logger.LogError($"{nameof(ServerData)} error: {ErrorMessage}");
         var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
        return Result.Fail(errorMessage);
    }
}