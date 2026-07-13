using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_API_Advanced.Models
{
    public class EmployeeRecord
    {
        [Required(ErrorMessage = "Employee Identifier is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive integer.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be non-negative.")]
        public decimal Salary { get; set; }

        public bool Permanent { get; set; }

        [Required(ErrorMessage = "Department association is required.")]
        public OrgUnit? Department { get; set; }

        public List<TechnicalSkill> Skills { get; set; } = new List<TechnicalSkill>();

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }
    }
}
