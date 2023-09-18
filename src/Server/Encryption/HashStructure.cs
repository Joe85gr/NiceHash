using System;
using System.Net.Http;

namespace Server.Encryption
{
    public class HashStructure
    {
        public HashStructure(string time, string endpoint, HttpMethod method, string guid, string bodyStr = null)
        {
            var splitEndpoint = endpoint.Split('?');
            
            Time = time;
            EncodedPath = splitEndpoint[0];
            Query = splitEndpoint.Length == 1 ? null : splitEndpoint[1];
            BodyStr = bodyStr;
            Nonce = guid;
            Method = MethodString(method);
            ApiSecret = Environment.GetEnvironmentVariable("NICEHASH_API_SECRET");
            ApiKey = Environment.GetEnvironmentVariable("NICEHASH_API_KEY");
            OrgId = Environment.GetEnvironmentVariable("NICEHASH_ORG_ID");
        }

        public string ApiSecret { get; }
        public string ApiKey { get; }
        public string Time { get; }
        public string Nonce { get; }
        public string OrgId { get; }
        public string EncodedPath { get; }
        public string Query { get; }
        public string BodyStr { get; }
        public string Method { get; }
        
        private static string MethodString(HttpMethod method) => 
            method == HttpMethod.Get ? "GET" : 
            method == HttpMethod.Post ? "POST" :
            method == HttpMethod.Put ? "PUT" :
            method == HttpMethod.Delete ? "DELETE" : 
            throw new ArgumentOutOfRangeException(nameof(HttpMethod), method, null);
    }
}
