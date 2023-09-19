using System.Text.Json.Serialization;

namespace Library.Models;

public class DeviceType
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class Status
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class Speed
{
    [JsonPropertyName("algorithm")]
    public string Algorithm { get; set; }

    [JsonPropertyName("speed")]
    public string HashSpeed { get; set; }

    [JsonPropertyName("displaySuffix")]
    public string DisplaySuffix { get; set; }
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

    [JsonPropertyName("powerUsage")]
    public double PowerUsage { get; set; }

    [JsonPropertyName("speeds")]
    public List<Speed> Speeds { get; set; }

}

public class MiningRig
{
    [JsonPropertyName("rigId")]
    public string RigId { get; set; }

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

    [JsonPropertyName("devices")]
    public List<Device> Devices { get; set; }

    [JsonPropertyName("cpuMiningEnabled")]
    public bool CpuMiningEnabled { get; set; }

    [JsonPropertyName("cpuExists")]
    public bool CpuExists { get; set; }
}

public class Rigs2
{
    [JsonPropertyName("totalProfitability")]
    public decimal TotalProfitability { get; set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; set; }

    [JsonPropertyName("nextPayoutTimestamp")]
    public DateTime NextPayoutTimestamp { get; set; }

    [JsonPropertyName("miningRigs")]
    public List<MiningRig> MiningRigs { get; set; }

    [JsonPropertyName("totalProfitabilityLocal")]
    public decimal TotalProfitabilityLocal { get; set; }
}