using System.Text.Json.Serialization;

namespace Library.Models;

public class Entry
{
    public Entry(string description)
    {
        Description = description;
    }

    [JsonPropertyName("description")]
    public string Description { get; private set; }
}

public class Speed
{
    public Speed(string algorithm, string hashSpeed, string displaySuffix)
    {
        Algorithm = algorithm;
        HashSpeed = hashSpeed;
        DisplaySuffix = displaySuffix;
    }

    [JsonPropertyName("algorithm")]
    public string Algorithm { get; private set; }

    [JsonPropertyName("speed")]
    public string HashSpeed { get; private set; }

    [JsonPropertyName("displaySuffix")]
    public string DisplaySuffix { get; private set; }
}

public class Device
{
    public Device(string name, Entry deviceType, Entry status, double temperature, double load, 
        double revolutionsPerMinute, double revolutionsPerMinutePercentage, double powerUsage, List<Speed> speeds)
    {
        Name = name;
        DeviceType = deviceType;
        Status = status;
        Temperature = temperature;
        Load = load;
        RevolutionsPerMinute = revolutionsPerMinute;
        RevolutionsPerMinutePercentage = revolutionsPerMinutePercentage;
        PowerUsage = powerUsage;
        Speeds = speeds;
    }

    [JsonPropertyName("name")]
    public string Name { get; private set; }

    [JsonPropertyName("deviceType")]
    public Entry DeviceType { get; private set; }

    [JsonPropertyName("status")]
    public Entry Status { get; private set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; private set; }

    [JsonPropertyName("load")]
    public double Load { get; private set; }

    [JsonPropertyName("revolutionsPerMinute")]
    public double RevolutionsPerMinute { get; private set; }

    [JsonPropertyName("revolutionsPerMinutePercentage")]
    public double RevolutionsPerMinutePercentage { get; private set; }

    [JsonPropertyName("powerUsage")]
    public double PowerUsage { get; private set; }

    [JsonPropertyName("speeds")]
    public List<Speed> Speeds { get; private set; }

}

public class MiningRig
{
    public MiningRig(string rigId, string name, long statusTime, long joinTime, string minerStatus, 
        string groupName, string unpaidAmount, List<Device> devices, bool cpuMiningEnabled, bool cpuExists)
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

    [JsonPropertyName("rigId")]
    public string RigId { get; private set; }

    [JsonPropertyName("name")]
    public string Name { get; private set; }

    [JsonPropertyName("statusTime")]
    public long StatusTime { get; private set; }

    [JsonPropertyName("joinTime")]
    public long JoinTime { get; private set; }

    [JsonPropertyName("minerStatus")]
    public string MinerStatus { get; private set; }

    [JsonPropertyName("groupName")]
    public string GroupName { get; private set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; private set; }

    [JsonPropertyName("devices")]
    public List<Device> Devices { get; private set; }

    [JsonPropertyName("cpuMiningEnabled")]
    public bool CpuMiningEnabled { get; private set; }

    [JsonPropertyName("cpuExists")]
    public bool CpuExists { get; private set; }
}

public class Rigs2
{
    public Rigs2(decimal totalProfitability, string unpaidAmount, DateTime nextPayoutTimestamp, 
        List<MiningRig> miningRigs, decimal totalProfitabilityLocal)
    {
        TotalProfitability = totalProfitability;
        UnpaidAmount = unpaidAmount;
        NextPayoutTimestamp = nextPayoutTimestamp;
        MiningRigs = miningRigs;
        TotalProfitabilityLocal = totalProfitabilityLocal;
    }

    [JsonPropertyName("totalProfitability")]
    public decimal TotalProfitability { get; private set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; private set; }

    [JsonPropertyName("nextPayoutTimestamp")]
    public DateTime NextPayoutTimestamp { get; private set; }

    [JsonPropertyName("miningRigs")]
    public List<MiningRig> MiningRigs { get; private set; }

    [JsonPropertyName("totalProfitabilityLocal")]
    public decimal TotalProfitabilityLocal { get; private set; }
}