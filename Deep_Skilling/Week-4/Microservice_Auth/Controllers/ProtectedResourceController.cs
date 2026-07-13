using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_Auth.Controllers
{
    [ApiController]
    [Route("api/secure")]
    [Authorize]
    public class ProtectedResourceController : ControllerBase
    {
        // GET: api/secure/resource
        [HttpGet("resource")]
        public IActionResult GetSecretData()
        {
            var user = User.Identity?.Name ?? "Unknown";
            return Ok(new
            {
                Message = "Access Granted! This resource is protected by JWT Authentication.",
                AuthorizedUser = user,
                Timestamp = System.DateTime.UtcNow
            });
        }
    }
}
