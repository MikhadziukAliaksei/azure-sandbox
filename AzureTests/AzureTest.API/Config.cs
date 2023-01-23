namespace AzureTest.API;

public static class Config
{
    public static TestConfig GetTestConfig(this IConfiguration configuration)
    {
        var testConfig = new TestConfig();
        configuration.GetSection(nameof(TestConfig)).Bind(testConfig);

        return testConfig;
    }
}