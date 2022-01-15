using System;
using Library.Models;

namespace Server.Encryption
{
    public class HashStructure
    {
        public HashStructure(string time, string endpoint, RequestMethod method, string bodyStr = null)
        {
            Time = time;
            EncodedPath = GetPath(endpoint);
            Query = GetQuery(endpoint);
            BodyStr = bodyStr;
            Nonce = Guid.NewGuid().ToString();
            Method = method.ToString();
            ApiSecret = Environment.GetEnvironmentVariable("NICEHASH_API_SECRET") ?? "hardcoded-api-secret";
            ApiKey = Environment.GetEnvironmentVariable("NICEHASH_API_KEY") ?? "hardcoded-api-key";
            OrgId  = Environment.GetEnvironmentVariable("NICEHASH_ORG_ID") ?? "hardcoded-org-id";
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

        private static string GetPath(string url)
        {
            var arrSplit = url.Split('?');
            return arrSplit[0];
        }
        private static string GetQuery(string url)
        {
            var arrSplit = url.Split('?');

            return arrSplit.Length == 1 ? null : arrSplit[1];
        }
    }
}
