namespace Library.Models
{
    public class NiceHashBalance
    {
        public NiceHashBalance(string currency, Dictionary<string, decimal> totals, 
            decimal available, int btcRate, decimal fiatRate)
        {
            Currency = currency;
            Totals = totals;
            Available = available;
            BtcRate = btcRate;
            FiatRate = fiatRate;
        }

        public string Currency { get; private set; }
        public Dictionary<string, decimal> Totals { get; private set; }

        public decimal Available { get; private set; }

        public int BtcRate { get; private set; }
        public decimal FiatRate { get; private set; }
    }
}
