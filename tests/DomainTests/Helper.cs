using System;
using Bogus;

namespace ServerTests.Configuration;

public static class Helper
{
    public static void ConfigureFakeEnvironmentalVariables()
    {
        var faker = new Faker();
        Environment.SetEnvironmentVariable("NICEHASH_API_SECRET", faker.Random.AlphaNumeric(12));
        Environment.SetEnvironmentVariable("NICEHASH_API_KEY", faker.Random.AlphaNumeric(65));
        Environment.SetEnvironmentVariable("NICEHASH_ORG_ID", faker.Random.AlphaNumeric(12));
    }
}