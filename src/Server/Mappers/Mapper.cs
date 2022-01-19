using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Server.Mappers
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
                    { "Total", Math.Round(Convert.ToDecimal(btcBalance.TotalBalance) + totalUnpad, 8)},
                    { "TotalFiat", Math.Round(totalBalance * btcBalance.FiatRate + totalUnpad * btcBalance.FiatRate, 2) },

                    { "TotalAvailable", Convert.ToDecimal(btcBalance.TotalBalance) },
                    { "TotalAvailableFiat", Math.Round(totalBalance * btcBalance.FiatRate, 2) },

                    { "TotalUnpaid", Math.Round(totalUnpad, 8) },
                    { "TotalUnpaidFiat", Math.Round(totalUnpad * btcBalance.FiatRate, 2) },
                },
            };

            var details = rigsDetails.MiningRigs.Select(rig => new NiceHashRigDetails
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
