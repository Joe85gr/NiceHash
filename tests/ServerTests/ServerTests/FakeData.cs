using System;
using System.Collections.Generic;
using Bogus;
using Library.Models;

namespace ServerTests;

public static class FakeData
{
    public static NiceHashData FakeNiceHashData()
    {
        var faker = new Faker<NiceHashData>()
            .RuleFor(x => x.Balance, FakeBalance)
            .RuleFor(x => x.NextPayoutTimestamp, x => x.Date.Future());

        return faker.Generate();
    }

    public static Dictionary<string, decimal> FakeTotals()
    {
        var totalsFaker = new Faker();
        
        var totals = new Dictionary<string, decimal>
        {
            {"Total", Math.Round(totalsFaker.Random.Decimal(), 8)},
            {"TotalFiat", Math.Round(totalsFaker.Random.Decimal(), 2)},

            {"TotalAvailable",  Math.Round(totalsFaker.Random.Decimal(), 2)},
            {"TotalAvailableFiat", Math.Round(totalsFaker.Random.Decimal(), 2)},

            {"TotalUnpaid", Math.Round(totalsFaker.Random.Decimal(), 8)},
            {"TotalUnpaidFiat", Math.Round(totalsFaker.Random.Decimal(), 2)},
        };

        return totals;
    }

    public static NiceHashBalance FakeBalance()
    {
        var balanceFaker = new Faker<NiceHashBalance>()
            .RuleFor(x => x.Available, x => x.Finance.Amount())
            .RuleFor(x => x.Currency, x => x.Finance.Locale)
            .RuleFor(x => x.BtcRate, x => x.Random.Int())
            .RuleFor(x => x.FiatRate, x => x.Finance.Amount())
            .RuleFor(x => x.Totals, FakeTotals);

        return balanceFaker.Generate();
    }

    public static NiceHashRigDetails FakeRigDetails()
    {
        var rigDetailsFaker = new Faker<NiceHashRigDetails>()
            .RuleFor(x => x.Name, x => x.Name.FirstName())
            .RuleFor(x => x.CpuExists, x => x.Random.Bool())
            .RuleFor(x => x.GroupName, x => x.Name.FirstName())
            .RuleFor(x => x.RigId, x => x.Name.FirstName())
            .RuleFor(x => x.UnpaidAmount, x => x.Finance.Amount())
            .RuleFor(x => x.CpuMiningEnabled, false);

        return rigDetailsFaker.Generate();
    }
}