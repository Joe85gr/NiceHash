using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace Server.Extensions;

public static class HttpClientExtensions
{
    private const int MaxRetries = 3;
    private const int RetryDelay = 500;

    public static async Task<T> GetContentAsync<T>(this HttpClient client, HttpRequestMessage request, 
        CancellationToken cancellationToken = default)
    {
        if (client is null) throw new Exception("Error: Client is null");
        
        var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetry(MaxRetries, attemptCount => TimeSpan.FromMilliseconds(attemptCount * RetryDelay));

        var response = await retryPolicy.Execute(() => client.SendAsync(request, cancellationToken));
        
        response.EnsureSuccessStatusCode();

        var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        
        var content = await JsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken: cancellationToken);

        return content;

    }
}