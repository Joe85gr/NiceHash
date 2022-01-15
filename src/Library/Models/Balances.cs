using System.Text.Json.Serialization;

namespace Library.Models
{
    public class Balances
    {
        [JsonPropertyName("total")]
        public Total Total { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currency?> Currencies { get; set; }
    }

    public class Total
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("totalBalance")]
        public string TotalBalance { get; set; }

        [JsonPropertyName("available")]
        public string Available { get; set; }

        [JsonPropertyName("debt")]
        public string Debt { get; set; }

        [JsonPropertyName("pending")]
        public string Pending { get; set; }
    }

    public class Currency
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("currency")]
        public string Curr { get; set; }

        [JsonPropertyName("totalBalance")]
        public string TotalBalance { get; set; }

        [JsonPropertyName("available")]
        public string Available { get; set; }

        [JsonPropertyName("debt")]
        public string Debt { get; set; }

        [JsonPropertyName("pending")]
        public string Pending { get; set; }

        [JsonPropertyName("btcRate")]
        public int BtcRate { get; set; }
        [JsonPropertyName("fiatRate")]
        public decimal FiatRate { get; set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }
    }
}
