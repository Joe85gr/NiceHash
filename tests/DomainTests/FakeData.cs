using Bogus;
using Library.Models;

namespace DomainTests;

public static class FakeData
{
    public static RigsActivity FakeNiceHashData()
    {
        var faker = new Faker<RigsActivity>()
            .RuleFor(x => x.Balance, FakeBalance)
            .RuleFor(x => x.NextPayoutTimestamp, x => x.Date.Future());

        return faker.Generate();
    }

    private static Dictionary<string, decimal> FakeTotals()
    {
        var totalsFaker = new Faker();
        
        var totals = new Dictionary<string, decimal>
        {
            {"Total", Math.Round(totalsFaker.Random.Decimal(), 8)},
            {"TotalFiat", Math.Round(totalsFaker.Random.Decimal(), 2)},

            {"TotalAvailable",  Math.Round(totalsFaker.Random.Decimal(), 2)},
            {"TotalAvailableFiat", Math.Round(totalsFaker.Random.Decimal(), 2)},

            {"TotalUnpaid", Math.Round(totalsFaker.Random.Decimal(), 8)},
            {"TotalUnpaidFiat", Math.Round(totalsFaker.Random.Decimal(), 2)}
        };

        return totals;
    }

    private static NiceHashBalance FakeBalance()
    {
        var balanceFaker = new Faker<NiceHashBalance>()
            .RuleFor(x => x.Available, x => x.Finance.Amount())
            .RuleFor(x => x.Currency, x => x.Finance.Locale)
            .RuleFor(x => x.BtcRate, x => x.Random.Int())
            .RuleFor(x => x.FiatRate, x => x.Finance.Amount())
            .RuleFor(x => x.Totals, FakeTotals);

        return balanceFaker.Generate();
    }
}