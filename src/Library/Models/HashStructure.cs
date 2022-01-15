namespace Library.Models
{
    public class HashStructure
    {
        public HashStructure(string? time, string endpoint, RequestMethod method, string? bodyStr = null)
        {
            Time = time;
            EncodedPath = GetPath(endpoint);
            Query = GetQuery(endpoint);
            BodyStr = bodyStr;
            Nonce = Guid.NewGuid().ToString();
            Method = method.ToString();
        }

        public string ApiSecret { get; } = Environment.GetEnvironmentVariable("NICEHASH_API_SECRET") ?? "hardcoded-api-secret";
        public string ApiKey { get; } = Environment.GetEnvironmentVariable("NICEHASH_API_KEY") ?? "hardcoded-api-key";
        public string? Time { get; }
        public string Nonce { get; }
        public string OrgId { get; } = Environment.GetEnvironmentVariable("NICEHASH_ORG_ID") ?? "hardcoded-org-id";
        public string EncodedPath { get; }
        public string? Query { get; }
        public string? BodyStr { get; }
        public string Method { get; }

        private static string GetPath(string url)
        {
            var arrSplit = url.Split('?');
            return arrSplit[0];
        }
        private static string? GetQuery(string url)
        {
            var arrSplit = url.Split('?');

            return arrSplit.Length == 1 ? null : arrSplit[1];
        }
    }
}
