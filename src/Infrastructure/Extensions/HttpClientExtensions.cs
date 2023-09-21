using System.Net;
using System.Text.Json;
using FluentResults;
using Polly;
using Polly.Retry;

namespace Infrastructure.Extensions;

public static class HttpClientExtensions
{
    private const int MaxRetries = 3;
    private const int RetryDelay = 500;
    
    private static RetryPolicy RetryPolicy => Policy.Handle<Exception>()
        .WaitAndRetry(MaxRetries, attemptCount => TimeSpan.FromMilliseconds(attemptCount * RetryDelay));

    public static async Task<Result<T>> GetContentAsync<T>(this HttpClient client, HttpRequestMessage request, 
        CancellationToken cancellationToken = default)
    {
        var response = await RetryPolicy.Execute(() => client.SendAsync(request, cancellationToken));

        return await GetResult<T>(response, cancellationToken);
    }
    
    public static async Task<Result<T>> GetContentAsync<T>(this HttpClient client, string endpoint, CancellationToken cancellationToken = default)
    {
        var response = await RetryPolicy.Execute(() => client.GetAsync(endpoint, cancellationToken));

        return await GetResult<T>(response, cancellationToken);
    }

    private static async Task<Result<T>> GetResult<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if(response.IsSuccessStatusCode == false) return ErrorResult<T>(response.StatusCode);
        
        var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        
        var content = await JsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken: cancellationToken);
        
        return content is null 
            ? Result.Fail("Unable to map data from NiceHash. Please check if their API has breaking changes.") 
            : Result.Ok(content);
    }
    
    private static Result<T> ErrorResult<T>(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.Forbidden or HttpStatusCode.Unauthorized => Result.Fail("Invalid NiceHash API Key."),
        HttpStatusCode.NotFound => Result.Fail("Unable to retrieve data from NiceHash. Not Found."),
        _ => Result.Fail("Unable to retrieve data from NiceHash. Please check if their API is down.")
    };
}