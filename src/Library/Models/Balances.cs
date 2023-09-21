using System.Text.Json.Serialization;

namespace Library.Models
{
    public class Balances
    {
        public Balances(Total total, List<Currency?> currencies)
        {
            Total = total;
            Currencies = currencies;
        }

        [JsonPropertyName("total")]
        public Total Total { get; private set; }

        [JsonPropertyName("currencies")]
        public List<Currency?> Currencies { get; private set; }
    }

    public class Total
    {
        public Total(string currency, string totalBalance, string available, string debt, string pending)
        {
            Currency = currency;
            TotalBalance = totalBalance;
            Available = available;
            Debt = debt;
            Pending = pending;
        }

        [JsonPropertyName("currency")]
        public string Currency { get; private set; }

        [JsonPropertyName("totalBalance")]
        public string TotalBalance { get; private set; }

        [JsonPropertyName("available")]
        public string Available { get; private set; }

        [JsonPropertyName("debt")]
        public string Debt { get; private set; }

        [JsonPropertyName("pending")]
        public string Pending { get; private set; }
    }

    public class Currency
    {
        public Currency(bool active, string curr, string totalBalance, string available, 
            string debt, string pending, int btcRate, decimal fiatRate, bool? enabled)
        {
            Active = active;
            Curr = curr;
            TotalBalance = totalBalance;
            Available = available;
            Debt = debt;
            Pending = pending;
            BtcRate = btcRate;
            FiatRate = fiatRate;
            Enabled = enabled;
        }

        [JsonPropertyName("active")]
        public bool Active { get; private set; }

        [JsonPropertyName("currency")]
        public string Curr { get; private set; }

        [JsonPropertyName("totalBalance")]
        public string TotalBalance { get; private set; }

        [JsonPropertyName("available")]
        public string Available { get; private set; }

        [JsonPropertyName("debt")]
        public string Debt { get; private set; }

        [JsonPropertyName("pending")]
        public string Pending { get; private set; }

        [JsonPropertyName("btcRate")]
        public int BtcRate { get; private set; }
        
        [JsonPropertyName("fiatRate")]
        public decimal FiatRate { get; private set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; private set; }
    }
}
