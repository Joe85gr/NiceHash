namespace Library.Models
{
    public class NiceHashRigDetails
    {
        public string RigId { get; set; }
        public string Name { get; set; }
        public long StatusTime { get; set; }
        public long JoinTime { get; set; }
        public string MinerStatus { get; set; }
        public string GroupName { get; set; }
        public decimal UnpaidAmount { get; set; }
        public List<RigDevice> Devices { get; set; }
        public bool CpuMiningEnabled { get; set; }
        public bool CpuExists { get; set; }
    }

    public class RigDevice
    {
        public string Name { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string> Stats { get; set; }
        public double Load { get; set; }
        public double FanSpeed { get; set; }
        public double FanPercentage { get; set; }
        public string DisplaySuffix { get; set; }
        public string Algorithm { get; set; }
    }
}
