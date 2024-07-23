using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //strongly typed Dependency Injection
        private readonly IMyLogger _logger;

        public DemoController()
        {
            _logger = new LogToDB();

        }

        [HttpGet]
        public  ActionResult Index()
        {
            _logger.Log("Index Method started ");
            return Ok();
        }
    }
}
