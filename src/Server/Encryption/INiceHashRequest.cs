using System.Net.Http;
using Library.Models;

namespace Server.Encryption;

public interface INiceHashRequest
{
    HttpRequestMessage GenerateRequest(string baseUrl, string endpoint, RequestMethod method, string serverTime);
}