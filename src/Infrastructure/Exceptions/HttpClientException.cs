namespace Infrastructure.Exceptions;

public class HttpClientException : Exception
{
    public HttpClientException(string? message=null, Exception? innerException=null) : base(message, innerException)
    {
        
    }
}