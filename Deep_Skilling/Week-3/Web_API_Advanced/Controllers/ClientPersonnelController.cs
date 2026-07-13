using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_Advanced.Models;

namespace Web_API_Advanced.Controllers
{
    [ApiController]
    [Route("api/ClientPersonnel")]
    public class ClientPersonnelController : ControllerBase
    {
        private static readonly List<EmployeeRecord> Employees = new()
        {
            new EmployeeRecord
            {
                Id = 1,
                Name = "Aiden Chen",
                Salary = 75000,
                Permanent = true,
                Department = new OrgUnit { Id = 10, Name = "Engineering" },
                Skills = new List<TechnicalSkill>
                {
                    new TechnicalSkill { Id = 1, Name = ".NET Core" },
                    new TechnicalSkill { Id = 2, Name = "SQL Server" }
                },
                DateOfBirth = new DateTime(1995, 8, 12)
            },
            new EmployeeRecord
            {
                Id = 2,
                Name = "Sophia Vargas",
                Salary = 68000,
                Permanent = false,
                Department = new OrgUnit { Id = 20, Name = "Human Resources" },
                Skills = new List<TechnicalSkill>
                {
                    new TechnicalSkill { Id = 3, Name = "Talent Acquisition" }
                },
                DateOfBirth = new DateTime(1998, 4, 25)
            }
        };

        // GET: api/ClientPersonnel
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Employees);
        }

        // PUT: api/ClientPersonnel/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeRecord))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeRecord updatedRecord)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee identifier.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRecord = Employees.FirstOrDefault(e => e.Id == id);
            if (existingRecord == null)
            {
                return NotFound($"Employee record with ID {id} was not found.");
            }

            // Sync values
            existingRecord.Name = updatedRecord.Name;
            existingRecord.Salary = updatedRecord.Salary;
            existingRecord.Permanent = updatedRecord.Permanent;
            existingRecord.Department = updatedRecord.Department;
            existingRecord.Skills = updatedRecord.Skills;
            existingRecord.DateOfBirth = updatedRecord.DateOfBirth;

            return Ok(existingRecord);
        }
    }
}
