using System.Globalization;
using System.Net;
using Azure.Data.AppConfiguration;

namespace AzureTest.API;

public static class IpConfiguration
{
    public static string ConfigureIpdAddress(string appConfigurationUrl)
    {
        // Retrieve host ip address
        var host = Dns.GetHostName();
        var hostAddressesList = Dns.GetHostAddresses(host);
        var currentIpAddress = $"http://{hostAddressesList[1]}:80";

        // Retrieve AppConfig service address
        var client = new ConfigurationClient(appConfigurationUrl);

        var storedIpAddress = client.GetConfigurationSetting("HttpConfiguration:UserServiceBaseUrl").Value.Value;

        if (storedIpAddress.Equals(currentIpAddress)) return currentIpAddress;

        client.SetConfigurationSetting("HttpConfiguration:UserServiceBaseUrl", currentIpAddress);

        client.SetConfigurationSetting("Ampligo:Settings:Sentinel",
            DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

        return currentIpAddress;
    }
}