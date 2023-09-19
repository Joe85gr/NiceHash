namespace Library.Models
{
    public class NiceHashRigDetails
    {
        public NiceHashRigDetails(string rigId, string name, long statusTime, long joinTime, string minerStatus, 
            string groupName, decimal unpaidAmount, List<RigDevice> devices, bool cpuMiningEnabled, bool cpuExists)
        {
            RigId = rigId;
            Name = name;
            StatusTime = statusTime;
            JoinTime = joinTime;
            MinerStatus = minerStatus;
            GroupName = groupName;
            UnpaidAmount = unpaidAmount;
            Devices = devices;
            CpuMiningEnabled = cpuMiningEnabled;
            CpuExists = cpuExists;
        }

        public string RigId { get; private set; }
        public string Name { get; private set; }
        public long StatusTime { get; private set; }
        public long JoinTime { get; private set; }
        public string MinerStatus { get; private set; }
        public string GroupName { get; private set; }
        public decimal UnpaidAmount { get; private set; }
        public List<RigDevice> Devices { get; private set; }
        public bool CpuMiningEnabled { get; private set; }
        public bool CpuExists { get; private set; }
    }

    public class RigDevice
    {
        public RigDevice(string name, string deviceType, string status, Dictionary<string, string> stats, 
            double load, double fanSpeed, double fanPercentage, string? displaySuffix, string? algorithm)
        {
            Name = name;
            DeviceType = deviceType;
            Status = status;
            Stats = stats;
            Load = load;
            FanSpeed = fanSpeed;
            FanPercentage = fanPercentage;
            DisplaySuffix = displaySuffix;
            Algorithm = algorithm;
        }

        public string Name { get; private set; }
        public string DeviceType { get; private set; }
        public string Status { get; private set; }
        public Dictionary<string, string> Stats { get; private set; }
        public double Load { get; private set; }
        public double FanSpeed { get; private set; }
        public double FanPercentage { get; private set; }
        public string? DisplaySuffix { get; private set; }
        public string? Algorithm { get; private set; }
    }
}
