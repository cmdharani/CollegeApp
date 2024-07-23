using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        
        private readonly IMyLogger _logger;


        ////strongly typed Dependency Injection
        //public DemoController()
        //{
        //    _logger = new LogToDB();

        //}

        //Lossly coupled typed Dependency Injection
        public DemoController(IMyLogger myLogger)
        {
            _logger = myLogger;
        }

        [HttpGet]
        public  ActionResult Index()
        {
            _logger.Log("Index Method started ");
            return Ok();
        }
    }
}
