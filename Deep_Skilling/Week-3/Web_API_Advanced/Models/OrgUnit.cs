using System.ComponentModel.DataAnnotations;

namespace Web_API_Advanced.Models
{
    public class OrgUnit
    {
        [Required(ErrorMessage = "Department ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        public string Name { get; set; } = string.Empty;
    }
}
