using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Advanced.Models;

namespace Web_API_Advanced.Controllers
{
    [ApiController]
    [Route("api/AuthorizedEmployee")]
    [Authorize(Roles = "Manager,Lead")]
    public class AuthorizedEmployeeController : ControllerBase
    {
        // GET: api/AuthorizedEmployee
        [HttpGet]
        public IActionResult GetSecretStaffData()
        {
            var reports = new List<object>
            {
                new { Id = 101, Name = "Admin Staff Member", MonthlyAllowance = 800.00 },
                new { Id = 102, Name = "Confidential Manager", MonthlyAllowance = 1200.00 }
            };
            return Ok(reports);
        }
    }
}
