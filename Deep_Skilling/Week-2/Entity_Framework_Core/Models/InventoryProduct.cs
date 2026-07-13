using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Framework_Core.Models
{
    public class InventoryProduct
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int StockLevel { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")]
        public CategoryItem? Category { get; set; }
    }
}
