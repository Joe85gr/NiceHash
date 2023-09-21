using System.Globalization;
using AutoBogus;
using Library.Models;

namespace DomainTests;

public static class FakeData
{
    public static Currency FakeCurrency()
    {
        return new AutoFaker<Currency>()
            .RuleFor(fake => fake.TotalBalance, fake => fake.Random.Decimal().ToString(CultureInfo.InvariantCulture))
            .RuleFor(fake => fake.Available, fake => fake.Random.Decimal().ToString(CultureInfo.InvariantCulture))
            .Generate();
    }
    
    public static Rigs2 FakeRigs2()
    {
        var speed = new AutoFaker<Speed>()
            .RuleFor(x => x.HashSpeed, fake => fake.Random.Double().ToString(CultureInfo.InvariantCulture));
        var device = new AutoFaker<Device>()
            .RuleFor(x => x.Speeds, new List<Speed> {speed});
        var miningRig = new AutoFaker<MiningRig>()
            .RuleFor(x => x.UnpaidAmount, fake => fake.Random.Decimal().ToString(CultureInfo.InvariantCulture))
            .RuleFor(x => x.Devices, new List<Device> {device})
            .Generate();
        var rigs2 = new AutoFaker<Rigs2>()
            .RuleFor(fake => fake.UnpaidAmount, fake => fake.Random.Int().ToString())
            .RuleFor(fake => fake.MiningRigs, new List<MiningRig> {miningRig})
            .Generate();

        return rigs2;
    }
}