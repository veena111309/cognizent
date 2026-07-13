using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Models
{
    public class CategoryItem
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property for related products
        public ICollection<InventoryProduct> Products { get; set; } = new List<InventoryProduct>();
    }
}
