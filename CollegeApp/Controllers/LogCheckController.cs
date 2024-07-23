using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogCheckController : ControllerBase
    {
        private readonly ILogger<LogCheckController> _logger;

        public LogCheckController(ILogger<LogCheckController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ActionResult Index()
        {

            _logger.LogTrace("Logging Trace Message");
            _logger.LogDebug("Logging Debug Message");
            _logger.LogInformation("Logging Information Message");
            _logger.LogWarning("Logging WArning Message");
            _logger.LogError("Logging Error Message");
            _logger.LogCritical("Logging Critical Message");

            return Ok();
        }
    }
}
