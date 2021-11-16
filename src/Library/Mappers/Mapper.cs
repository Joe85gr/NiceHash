using Library.Models;

namespace Library.Mappers
{
    public static class Mapper
    {
        public static NiceHashData MapNiceHashDataAsync(Currency btcBalance, Rigs2 rigsDetails)
        {
            var totalUnpad = Convert.ToDecimal(rigsDetails.UnpaidAmount);
            var totalBalance = Convert.ToDecimal(btcBalance.TotalBalance);

            var balance = new NiceHashBalance
            {
                Available = Convert.ToDecimal(btcBalance.Available),
                BtcRate = btcBalance.BtcRate,
                Currency = btcBalance.Curr,
                FiatRate = btcBalance.FiatRate,
                Totals = new Dictionary<string, decimal>
                {
                    { "TotalBalance", Convert.ToDecimal(btcBalance.TotalBalance) },
                    { "TotalUnpaid", Math.Round(totalUnpad, 8) },
                    { "TotalUnpaidFiat", Math.Round(totalUnpad * btcBalance.FiatRate, 2) },
                    { "TotalBalanceFiat", Math.Round(totalBalance * btcBalance.FiatRate, 2) },
                },
            };

            var details = rigsDetails.MiningRigs.Select(det => new NiceHashRigDetails
            {
                CpuExists = det.CpuExists,
                CpuMiningEnabled = det.CpuMiningEnabled,
                GroupName = det.GroupName,
                JoinTime = det.JoinTime,
                MinerStatus = det.MinerStatus,
                Name = det.Name,
                RigId = det.RigId,
                StatusTime = det.StatusTime,
                UnpaidAmount = Math.Round(Convert.ToDecimal(det.UnpaidAmount), 8),
                Devices = det.Devices.Select(d => new RigDevice
                {
                    DeviceType = d.DeviceType.Description,
                    FanPercentage = d.RevolutionsPerMinute,
                    FanSpeed = d.RevolutionsPerMinutePercentage,
                    Load = d.Load,
                    Name = d.Name,
                    Algorithm = d.Speeds.FirstOrDefault()?.Algorithm,
                    DisplaySuffix = d.Speeds?.FirstOrDefault()?.DisplaySuffix,
                    Status = d.Status.Description,

                    Stats = new Dictionary<string, string>
                    {
                        { "GPU Temperture", Math.Round(d.Temperature % 65536, 2) + "°C" },
                        { "VRAM Temperture", Math.Round(d.Temperature / 65536, 2) + "°C" },
                        { "Speed", Math.Round(Convert.ToDecimal(d.Speeds?.FirstOrDefault()?.HashSpeed), 2)+ " MH/s" },
                        { "Power Usage", d.PowerUsage + "W" },
                        { "Efficiency", Math.Round(Convert.ToDouble(d.Speeds?.FirstOrDefault()?.HashSpeed) / d.PowerUsage, 3) + " MH/J" },
                    }
                }).ToList()
            }).ToList();

            return new NiceHashData()
            {
                NextPayoutTimestamp = Convert.ToDateTime(rigsDetails.NextPayoutTimestamp),
                Balance = balance,
                RigsDetails = details,
                Profitability = new Dictionary<string, decimal>
                {
                    { "MedianActualProfitability", Math.Round(rigsDetails.TotalProfitability, 8) },
                    { "MedianLocalProfitability", Math.Round(rigsDetails.TotalProfitabilityLocal, 8) },
                    { "MedianActualProfitabilityFiat", Math.Round(rigsDetails.TotalProfitability * balance.FiatRate, 2) },
                    { "MedianLocalProfitabilityFiat", Math.Round(rigsDetails.TotalProfitabilityLocal * balance.FiatRate, 2) },
                }
            };
        }
    }
}
