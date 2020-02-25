using System.Threading.Tasks;
using AwsLambda.Models;
using Boro2g.Core.Environment;
using Microsoft.AspNetCore.Mvc;

namespace AwsLambda.Controllers
{
    // https://jssyourdatayourway.aws.boro2g.co.uk/api/jss/content
    [Route("api/[controller]")]
    public class JssController
    {
        private readonly IEnvironmentVariables _environmentVariables;

        public JssController(IEnvironmentVariables environmentVariables)
        {
            _environmentVariables = environmentVariables;
        }

        [HttpGet("content")]
        public async Task<ContentModel> Content()
        {
            var delayMs = _environmentVariables.GetInt("delayMs", 0);

            await Task.Delay(delayMs);

            return new ContentModel
            {
                Content = _environmentVariables.GetString("content", "Hello Jss"),
                DelayMs = delayMs,
            };
        }
    }
}
