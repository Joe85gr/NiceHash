using System.Text.Json.Serialization;

namespace Library.Models
{
    public class MinerStatuses
    {
        [JsonPropertyName("MINING")]
        public double MINING { get; set; }
    }

    public class RigTypes
    {
        [JsonPropertyName("MANAGED")]
        public double MANAGED { get; set; }
    }

    public class DevicesStatuses
    {
        [JsonPropertyName("MINING")]
        public double MINING { get; set; }

        [JsonPropertyName("DISABLED")]
        public double DISABLED { get; set; }
    }

    public class DeviceType
    {
        [JsonPropertyName("enumName")]
        public string EnumName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("enumName")]
        public string EnumName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class PowerMode
    {
        [JsonPropertyName("enumName")]
        public string EnumName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Speed
    {
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("speed")]
        public string HashSpeed { get; set; }

        [JsonPropertyName("displaySuffix")]
        public string DisplaySuffix { get; set; }
    }

    public class Intensity
    {
        [JsonPropertyName("enumName")]
        public string EnumName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Device
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("deviceType")]
        public DeviceType DeviceType { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("load")]
        public double Load { get; set; }

        [JsonPropertyName("revolutionsPerMinute")]
        public double RevolutionsPerMinute { get; set; }

        [JsonPropertyName("revolutionsPerMinutePercentage")]
        public double RevolutionsPerMinutePercentage { get; set; }

        [JsonPropertyName("powerMode")]
        public PowerMode PowerMode { get; set; }

        [JsonPropertyName("powerUsage")]
        public double PowerUsage { get; set; }

        [JsonPropertyName("speeds")]
        public List<Speed> Speeds { get; set; }

        [JsonPropertyName("intensity")]
        public Intensity Intensity { get; set; }

        [JsonPropertyName("nhqm")]
        public string Nhqm { get; set; }
    }

    public class Algorithm
    {
        [JsonPropertyName("enumName")]
        public string EnumName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("statsTime")]
        public long StatsTime { get; set; }

        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("algorithm")]
        public Algorithm Algorithm { get; set; }

        [JsonPropertyName("unpaidAmount")]
        public string UnpaidAmount { get; set; }

        [JsonPropertyName("difficulty")]
        public double Difficulty { get; set; }

        [JsonPropertyName("proxyId")]
        public double ProxyId { get; set; }

        [JsonPropertyName("timeConnected")]
        public long TimeConnected { get; set; }

        [JsonPropertyName("xnsub")]
        public bool Xnsub { get; set; }

        [JsonPropertyName("speedAccepted")]
        public double SpeedAccepted { get; set; }

        [JsonPropertyName("speedRejectedR1Target")]
        public double SpeedRejectedR1Target { get; set; }

        [JsonPropertyName("speedRejectedR2Stale")]
        public double SpeedRejectedR2Stale { get; set; }

        [JsonPropertyName("speedRejectedR3Duplicate")]
        public double SpeedRejectedR3Duplicate { get; set; }

        [JsonPropertyName("speedRejectedR4NTime")]
        public double SpeedRejectedR4NTime { get; set; }

        [JsonPropertyName("speedRejectedR5Other")]
        public double SpeedRejectedR5Other { get; set; }

        [JsonPropertyName("speedRejectedTotal")]
        public double SpeedRejectedTotal { get; set; }

        [JsonPropertyName("profitability")]
        public double Profitability { get; set; }
    }

    public class MiningRig
    {
        [JsonPropertyName("rigId")]
        public string RigId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("statusTime")]
        public long StatusTime { get; set; }

        [JsonPropertyName("joinTime")]
        public long JoinTime { get; set; }

        [JsonPropertyName("minerStatus")]
        public string MinerStatus { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("unpaidAmount")]
        public string UnpaidAmount { get; set; }

        [JsonPropertyName("notifications")]
        public List<string> Notifications { get; set; }

        [JsonPropertyName("softwareVersions")]
        public string SoftwareVersions { get; set; }

        [JsonPropertyName("devices")]
        public List<Device> Devices { get; set; }

        [JsonPropertyName("cpuMiningEnabled")]
        public bool CpuMiningEnabled { get; set; }

        [JsonPropertyName("cpuExists")]
        public bool CpuExists { get; set; }

        [JsonPropertyName("stats")]
        public List<Stat> Stats { get; set; }

        [JsonPropertyName("profitability")]
        public double Profitability { get; set; }

        [JsonPropertyName("localProfitability")]
        public double LocalProfitability { get; set; }

        [JsonPropertyName("rigPowerMode")]
        public string RigPowerMode { get; set; }
    }

    public class Pagination
    {
        [JsonPropertyName("size")]
        public double Size { get; set; }

        [JsonPropertyName("page")]
        public double Page { get; set; }

        [JsonPropertyName("totalPageCount")]
        public double TotalPageCount { get; set; }
    }

    public class Rigs2
    {
        [JsonPropertyName("minerStatuses")]
        public MinerStatuses MinerStatuses { get; set; }

        [JsonPropertyName("rigTypes")]
        public RigTypes RigTypes { get; set; }

        [JsonPropertyName("totalRigs")]
        public double TotalRigs { get; set; }

        [JsonPropertyName("totalProfitability")]
        public decimal TotalProfitability { get; set; }

        [JsonPropertyName("groupPowerMode")]
        public string GroupPowerMode { get; set; }

        [JsonPropertyName("totalDevices")]
        public double TotalDevices { get; set; }

        [JsonPropertyName("devicesStatuses")]
        public DevicesStatuses DevicesStatuses { get; set; }

        [JsonPropertyName("unpaidAmount")]
        public string UnpaidAmount { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("btcAddress")]
        public string BtcAddress { get; set; }

        [JsonPropertyName("nextPayoutTimestamp")]
        public DateTime NextPayoutTimestamp { get; set; }

        [JsonPropertyName("lastPayoutTimestamp")]
        public DateTime LastPayoutTimestamp { get; set; }

        [JsonPropertyName("miningRigGroups")]
        public List<object> MiningRigGroups { get; set; }

        [JsonPropertyName("miningRigs")]
        public List<MiningRig> MiningRigs { get; set; }

        [JsonPropertyName("rigNhmVersions")]
        public List<string> RigNhmVersions { get; set; }

        [JsonPropertyName("externalAddress")]
        public bool ExternalAddress { get; set; }

        [JsonPropertyName("totalProfitabilityLocal")]
        public decimal TotalProfitabilityLocal { get; set; }

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }
}
