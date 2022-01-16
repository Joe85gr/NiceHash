using System;
using System.Net.Http;
using Library.Models;
using Library.Services;

namespace Server.Encryption;

public class NiceHashRequest : INiceHashRequest
{
    private readonly IGuidService _guidService;

    public NiceHashRequest(IGuidService guidService)
    {
        _guidService = guidService;
    }
    public HttpRequestMessage GenerateRequest(string baseUrl, string endpoint, RequestMethod method, string serverTime)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(baseUrl + endpoint)
        };

        var guid = _guidService.NewGuid().ToString();
        var hashStructure = new HashStructure(serverTime, "/" + endpoint, method, guid);

        var text = Sha256Encryption.GenerateTextToHash(hashStructure);
        var encryptedHash = Sha256Encryption.GenerateHash(text, hashStructure.ApiSecret);

        request.Headers.Add("X-Time", serverTime);
        request.Headers.Add("X-Nonce", hashStructure.Nonce);
        request.Headers.Add("X-Auth", hashStructure.ApiKey + ":" + encryptedHash);
        request.Headers.Add("X-Organization-Id", hashStructure.OrgId);

        return request;
    }
}