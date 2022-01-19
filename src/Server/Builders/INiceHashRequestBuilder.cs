using System.Net.Http;
using Library.Models;

namespace Server.Builders;

public interface INiceHashRequestBuilder
{
    HttpRequestMessage GenerateRequest(string baseUrl, string endpoint, RequestMethod method, string serverTime);
}