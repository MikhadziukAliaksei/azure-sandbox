using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureTest.Function;

public static class BaseSbQueueTrigger
{
    [FunctionName("BaseSbQueueTrigger")]
    public static async Task RunAsync(
        [ServiceBusTrigger("namequeue",
            Connection =
                "ServiceBusConnection")]
        string myQueueItem, ILogger log)
    {
        log.LogInformation("C# ServiceBus queue trigger function processed message: {MyQueueItem}", myQueueItem);

        log.LogInformation("My name is {MyQueueItem}", myQueueItem);
    }
}