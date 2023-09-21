namespace Library.Models;

public class RigsActivity
{
    public List<NiceHashRigDetails>? RigsDetails { get; init; }
    public NiceHashBalance? Balance { get; init; }
    public Dictionary<string, decimal>? Profitability { get; init; }
    public DateTimeOffset NextPayoutTimestamp { get; init; }
        
    public static RigsActivity Map(Currency btcBalance, Rigs2 rigsDetails)
    {
        var totalUnpaid = Convert.ToDecimal(rigsDetails.UnpaidAmount);
        var totalBalance = Convert.ToDecimal(btcBalance.TotalBalance);

        var balance = GetBalance(btcBalance, totalUnpaid, totalBalance);

        var details = GetDetails(rigsDetails);

        return new RigsActivity
        {
            NextPayoutTimestamp = Convert.ToDateTime(rigsDetails.NextPayoutTimestamp),
            Balance = balance,
            RigsDetails = details,
            Profitability = new Dictionary<string, decimal>
            {
                {"MedianActualProfitability", Math.Round(rigsDetails.TotalProfitability, 8)},
                {"MedianLocalProfitability", Math.Round(rigsDetails.TotalProfitabilityLocal, 8)},
                {"MedianActualProfitabilityFiat", Math.Round(rigsDetails.TotalProfitability * balance.FiatRate, 2)},
                {
                    "MedianLocalProfitabilityFiat",
                    Math.Round(rigsDetails.TotalProfitabilityLocal * balance.FiatRate, 2)
                }
            }
        };
    }
    
    public static RigsActivity Clone(RigsActivity other)
    {
        return new RigsActivity
        {
            RigsDetails = other.RigsDetails,
            Balance = other.Balance,
            Profitability = other.Profitability,
            NextPayoutTimestamp = other.NextPayoutTimestamp
        };
    }

    private static List<NiceHashRigDetails> GetDetails(Rigs2 rigsDetails)
    {
        return rigsDetails.MiningRigs.Select(rig => new NiceHashRigDetails
        (
            cpuExists: rig.CpuExists,
            cpuMiningEnabled: rig.CpuMiningEnabled,
            groupName: rig.GroupName,
            joinTime: rig.JoinTime,
            minerStatus: rig.MinerStatus,
            name: rig.Name,
            rigId: rig.RigId,
            statusTime: rig.StatusTime,
            unpaidAmount: Math.Round(Convert.ToDecimal(rig.UnpaidAmount), 8),
            devices: rig.Devices.Select(device => new RigDevice
            (
                deviceType: device.DeviceType.Description,
                fanPercentage: device.RevolutionsPerMinute,
                fanSpeed: device.RevolutionsPerMinutePercentage,
                load: device.Load,
                name: device.Name,
                algorithm: device.Speeds.FirstOrDefault()?.Algorithm,
                displaySuffix: device.Speeds.FirstOrDefault()?.DisplaySuffix,
                status: device.Status.Description,
                stats: new Dictionary<string, string>
                {
                    {"GPU Temperature", Math.Round(device.Temperature % 65536, 2) + "°C"},
                    {"VRAM Temperature", Math.Round(device.Temperature / 65536, 2) + "°C"},
                    {"Speed", Math.Round(Convert.ToDecimal(device.Speeds.FirstOrDefault()?.HashSpeed), 2) + " MH/s"},
                    {"Power Usage", device.PowerUsage + "W"},
                    {
                        "Efficiency",
                        Math.Round(Convert.ToDouble(device.Speeds.FirstOrDefault()?.HashSpeed) / device.PowerUsage, 3) +
                        " MH/J"
                    }
                }
            )).ToList()
        )).ToList();
    }

    private static NiceHashBalance GetBalance(Currency btcBalance, decimal totalUnpaid, decimal totalBalance)
    {
        var totals = new Dictionary<string, decimal>
        {
            {"Total", Math.Round(Convert.ToDecimal(btcBalance.TotalBalance) + totalUnpaid, 8)},
            {"TotalFiat", Math.Round(totalBalance * btcBalance.FiatRate + totalUnpaid * btcBalance.FiatRate, 2)},

            {"TotalAvailable", Convert.ToDecimal(btcBalance.TotalBalance)},
            {"TotalAvailableFiat", Math.Round(totalBalance * btcBalance.FiatRate, 2)},

            {"TotalUnpaid", Math.Round(totalUnpaid, 8)},
            {"TotalUnpaidFiat", Math.Round(totalUnpaid * btcBalance.FiatRate, 2)}
        };
        
        return new NiceHashBalance(
            btcBalance.Curr,
            totals, 
            Convert.ToDecimal(btcBalance.Available), 
            btcBalance.BtcRate,
            btcBalance.FiatRate);
    }
}