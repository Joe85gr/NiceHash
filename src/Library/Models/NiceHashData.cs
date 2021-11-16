namespace Library.Models
{
    public class NiceHashData
    {
        public List<NiceHashRigDetails> RigsDetails { get; set; }
        public NiceHashBalance Balance { get; set; }
        public Dictionary<string, decimal> Profitability { get; set; }
        public DateTime NextPayoutTimestamp { get; set; }
    }
}
