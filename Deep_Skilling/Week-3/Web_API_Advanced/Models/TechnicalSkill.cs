using System.ComponentModel.DataAnnotations;

namespace Web_API_Advanced.Models
{
    public class TechnicalSkill
    {
        [Required(ErrorMessage = "Skill ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill Name is required.")]
        public string Name { get; set; } = string.Empty;
    }
}
