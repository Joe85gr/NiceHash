using System;
using System.Net.Http;
using Library.Models;

namespace Server.Encryption;

public static class RequestCredentials
{
    public static HttpRequestMessage SetNiceHashRequestWithCredentials(string baseUrl, string endpoint, RequestMethod method, string serverTime)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(baseUrl + endpoint)
        };

        var hashStructure = new HashStructure(serverTime, "/" + endpoint, method);
        var encryptedHash = Sha256Encryption.GenerateEncryptedHash(hashStructure);

        request.Headers.Add("X-Time", serverTime);
        request.Headers.Add("X-Nonce", hashStructure.Nonce);
        request.Headers.Add("X-Auth", hashStructure.ApiKey + ":" + encryptedHash);
        request.Headers.Add("X-Organization-Id", hashStructure.OrgId);

        return request;
    }
}