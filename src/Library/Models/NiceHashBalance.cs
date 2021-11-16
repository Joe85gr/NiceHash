namespace Library.Models
{
    public class NiceHashBalance
    {
        public string Currency { get; set; }
        public Dictionary<string, decimal> Totals { get; set; }

        public decimal Available { get; set; }

        public int BtcRate { get; set; }
        public decimal FiatRate { get; set; }
    }
}
