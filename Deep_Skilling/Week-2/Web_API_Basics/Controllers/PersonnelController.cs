using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API_Basics.Controllers
{
    [ApiController]
    [Route("api/Personnel")]
    public class PersonnelController : ControllerBase
    {
        private static readonly List<string> StaffNames = new()
        {
            "Aiden Chen",
            "Liam OConnor",
            "Sophia Vargas",
            "Zoe Mitchell"
        };

        // GET: api/Personnel
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        public IActionResult GetAllStaff()
        {
            return Ok(StaffNames);
        }

        // GET: api/Personnel/{index}
        [HttpGet("{index:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStaffByIndex(int index)
        {
            if (index < 0 || index >= StaffNames.Count)
            {
                return BadRequest("Invalid index position specified.");
            }
            return Ok(StaffNames[index]);
        }

        // POST: api/Personnel
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterStaff([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Staff name cannot be empty.");
            }
            StaffNames.Add(name);
            return StatusCode(StatusCodes.Status201Created, "Staff member successfully registered.");
        }

        // PUT: api/Personnel/{index}
        [HttpPut("{index:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ModifyStaff(int index, [FromBody] string name)
        {
            if (index < 0 || index >= StaffNames.Count)
            {
                return BadRequest("Invalid index position specified.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("New name value cannot be empty.");
            }
            StaffNames[index] = name;
            return Ok("Staff record modified successfully.");
        }

        // DELETE: api/Personnel/{index}
        [HttpDelete("{index:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DismissStaff(int index)
        {
            if (index < 0 || index >= StaffNames.Count)
            {
                return BadRequest("Invalid index position specified.");
            }
            string terminatedUser = StaffNames[index];
            StaffNames.RemoveAt(index);
            return Ok($"Staff record for '{terminatedUser}' deleted successfully.");
        }
    }
}
