using System.Net.Http;
using Library.Models;

namespace Server.Orchestrators;

public interface INiceHashRequestOrchestrator
{
    HttpRequestMessage GenerateRequest(string baseUrl, string endpoint, RequestMethod method, string serverTime);
}