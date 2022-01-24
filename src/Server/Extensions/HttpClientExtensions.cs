using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> GetContentAsync<T>(this HttpClient client, HttpRequestMessage request, 
        CancellationToken cancellationToken = default)
    {
        if (client is null) throw new Exception("Error: Client is null");
        
        var response = await client.SendAsync(request, cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var content = await JsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken: cancellationToken);

        return content;

    }
}