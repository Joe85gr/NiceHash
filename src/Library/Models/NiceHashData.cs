namespace Library.Models
{
    public class NiceHashData
    {
        public List<NiceHashRigDetails>? RigsDetails { get; set; }
        public NiceHashBalance? Balance { get; set; }
        public Dictionary<string, decimal>? Profitability { get; set; }
        public DateTime NextPayoutTimestamp { get; set; }
        
        public static NiceHashData Map(Currency btcBalance, Rigs2 rigsDetails)
        {
            var totalUnpaid = Convert.ToDecimal(rigsDetails.UnpaidAmount);
            var totalBalance = Convert.ToDecimal(btcBalance.TotalBalance);

            var balance = GetBalance(btcBalance, totalUnpaid, totalBalance);

            var details = GetDetails(rigsDetails);

            return new NiceHashData
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
                    },
                }
            };

        }

        private static List<NiceHashRigDetails> GetDetails(Rigs2 rigsDetails)
        {
            return rigsDetails.MiningRigs.Select(rig => new NiceHashRigDetails
            {
                CpuExists = rig.CpuExists,
                CpuMiningEnabled = rig.CpuMiningEnabled,
                GroupName = rig.GroupName,
                JoinTime = rig.JoinTime,
                MinerStatus = rig.MinerStatus,
                Name = rig.Name,
                RigId = rig.RigId,
                StatusTime = rig.StatusTime,
                UnpaidAmount = Math.Round(Convert.ToDecimal(rig.UnpaidAmount), 8),
                Devices = rig.Devices.Select(device => new RigDevice
                {
                    DeviceType = device.DeviceType.Description,
                    FanPercentage = device.RevolutionsPerMinute,
                    FanSpeed = device.RevolutionsPerMinutePercentage,
                    Load = device.Load,
                    Name = device.Name,
                    Algorithm = device.Speeds.FirstOrDefault()?.Algorithm,
                    DisplaySuffix = device.Speeds?.FirstOrDefault()?.DisplaySuffix,
                    Status = device.Status.Description,

                    Stats = new Dictionary<string, string>
                    {
                        { "GPU Temperature", Math.Round(device.Temperature % 65536, 2) + "°C" },
                        { "VRAM Temperature", Math.Round(device.Temperature / 65536, 2) + "°C" },
                        { "Speed", Math.Round(Convert.ToDecimal(device.Speeds?.FirstOrDefault()?.HashSpeed), 2)+ " MH/s" },
                        { "Power Usage", device.PowerUsage + "W" },
                        { "Efficiency", Math.Round(Convert.ToDouble(device.Speeds?.FirstOrDefault()?.HashSpeed) / device.PowerUsage, 3) + " MH/J" },
                    }
                }).ToList()
            }).ToList();
        }

        private static NiceHashBalance GetBalance(Currency btcBalance, decimal totalUnpaid, decimal totalBalance)
        {
            return new NiceHashBalance
            {
                Available = Convert.ToDecimal(btcBalance.Available),
                BtcRate = btcBalance.BtcRate,
                Currency = btcBalance.Curr,
                FiatRate = btcBalance.FiatRate,
                Totals = new Dictionary<string, decimal>
                {
                    { "Total", Math.Round(Convert.ToDecimal(btcBalance.TotalBalance) + totalUnpaid, 8)},
                    { "TotalFiat", Math.Round(totalBalance * btcBalance.FiatRate + totalUnpaid * btcBalance.FiatRate, 2) },

                    { "TotalAvailable", Convert.ToDecimal(btcBalance.TotalBalance) },
                    { "TotalAvailableFiat", Math.Round(totalBalance * btcBalance.FiatRate, 2) },

                    { "TotalUnpaid", Math.Round(totalUnpaid, 8) },
                    { "TotalUnpaidFiat", Math.Round(totalUnpaid * btcBalance.FiatRate, 2) },
                },
            };
        }

        public static NiceHashData Clone(NiceHashData other)
        {
            return new NiceHashData
            {
                RigsDetails = other.RigsDetails,
                Balance = other.Balance,
                Profitability = other.Profitability,
                NextPayoutTimestamp = other.NextPayoutTimestamp,
            };
        }
    }
}
