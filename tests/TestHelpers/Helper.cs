namespace TestHelpers;

public static class Helper
{
    public static void ConfigureFakeEnvironmentalVariables()
    {
        Environment.SetEnvironmentVariable("NICEHASH_API_SECRET", "some-api-secret");
        Environment.SetEnvironmentVariable("NICEHASH_API_KEY", "some-api-key");
        Environment.SetEnvironmentVariable("NICEHASH_ORG_ID", "some-org-id");
    }
}