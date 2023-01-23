using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureTest.Function;

public class BaseHttpTrigger
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationRefresher _configurationRefresher;

    public BaseHttpTrigger(IConfiguration configuration, IConfigurationRefresherProvider configurationRefresher)
    {
        _configuration = configuration;
        _configurationRefresher = configurationRefresher.Refreshers.First();
    }

    [FunctionName("BaseHttpTrigger")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        await _configurationRefresher.TryRefreshAsync();

        string name = req.Query["name"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name ??= data?.name;

        return name != null
            ? new OkObjectResult($"Hello, {_configuration["MyApp:Settings:Name"]}")
            : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
    }
}