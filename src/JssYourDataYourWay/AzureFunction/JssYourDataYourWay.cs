using System;
using System.Threading.Tasks;
using AzureFunction.Models;
using Boro2g.Core.Environment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunction
{
    // https://jssyourdatayourway.azure.boro2g.co.uk/api/JssYourDataYourWay

    public static class JssYourDataYourWay
    {
        [FunctionName("JssYourDataYourWay")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var environmentVariables = new EnvironmentVariables();

            var delayMs = environmentVariables.GetInt("delayMs", 0);

            await Task.Delay(delayMs);

            return new OkObjectResult(new ContentModel
            {
                Content = $"{environmentVariables.GetString("content", "Hello Jss Azure")} {DateTime.UtcNow.ToString("s")}",
                DelayMs = delayMs,
            });
        }
    }
}
