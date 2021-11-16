namespace Library.Models
{
    public class HashStructure
    {
        public HashStructure(string time, string endpoint, RequestMethod method, string? bodyStr = null)
        {
            Time = time;
            EncodedPath = GetPath(endpoint);
            Query = GetQuery(endpoint);
            BodyStr = bodyStr;
            Nonce = Guid.NewGuid().ToString();
            Method = method.ToString();
        }

        public string ApiSecret { get; set; } = Environment.GetEnvironmentVariable("NICEHASH_API_SECRET") ?? "hardcoded-api-secret";
        public string ApiKey { get; set; } = Environment.GetEnvironmentVariable("NICEHASH_API_KEY") ?? "hardcoded-api-key";
        public string Time { get; private set; }
        public string Nonce { get; private set; }
        public string OrgId { get; set; } = Environment.GetEnvironmentVariable("NICEHASH_ORG_ID") ?? "hardcoded-org-id";
        public string EncodedPath { get; private set; }
        public string? Query { get; private set; }
        public string? BodyStr { get; private set; }
        public string Method { get; private set; }

        private static string GetPath(string url)
        {
            var arrSplit = url.Split('?');
            return arrSplit[0];
        }
        private static string? GetQuery(string url)
        {
            var arrSplit = url.Split('?');

            if (arrSplit.Length == 1) return null;
            
            return arrSplit[1];
        }
    }
}
