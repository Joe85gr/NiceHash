using System;
using System.Net.Http;
using Server.Encryption;

namespace Server.Builders;

public class NiceHashRequestBuilder
{
    private HttpRequestMessage _request = new();

    public NiceHashRequestBuilder WithUri(string baseUrl, string endpoint)
    {
        _request.RequestUri = new Uri(baseUrl + endpoint);

        return this;
    }
    
    public NiceHashRequestBuilder WithHeaders(string serverTime, HashStructure hashStructure)
    {
        var text = Sha256Encryption.GenerateTextToHash(hashStructure);
        var encryptedHash = Sha256Encryption.GenerateHash(text, hashStructure.ApiSecret);
    
        _request.Headers.Add("X-Time", serverTime);
        _request.Headers.Add("X-Nonce", hashStructure.Nonce);
        _request.Headers.Add("X-Auth", hashStructure.ApiKey + ":" + encryptedHash);
        _request.Headers.Add("X-Organization-Id", hashStructure.OrgId);
    
        return this;
    }
    
    public NiceHashRequestBuilder WithMethod(HttpMethod method)
    {
        _request.Method = method;
        
        return this;
    }
    
    public HttpRequestMessage Build() => _request;
}