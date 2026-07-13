using Microsoft.AspNetCore.Mvc;

namespace Web_API_Basics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppStatusController : ControllerBase
    {
        // GET: api/AppStatus
        [HttpGet]
        public IActionResult GetStatus()
        {
            var systemDetails = new
            {
                Status = "Online",
                Service = "Corporate Inventory API",
                Environment = "Development",
                Version = "1.0.0"
            };
            return Ok(systemDetails);
        }

        // GET: api/AppStatus/ping
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong! Services are responsive.");
        }
    }
}
