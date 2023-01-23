using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureTest.Function.Startup))]

namespace AzureTest.Function;

public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        var cs = Environment.GetEnvironmentVariable("AzureAppConfigurationConnectionString");
        builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
        {
            options.Connect(cs)
                .ConfigureRefresh(refresh =>
                {
                    refresh.Register("MyApp:Settings:Sentinel", refreshAll: true)
                        .SetCacheExpiration(TimeSpan.FromSeconds(3));
                });
        });
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddAzureAppConfiguration();
        builder.Services.AddLogging();
    }
}