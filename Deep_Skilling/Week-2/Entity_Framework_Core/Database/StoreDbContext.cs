using Microsoft.EntityFrameworkCore;
using Entity_Framework_Core.Models;

namespace Entity_Framework_Core.Database
{
    public class StoreDbContext : DbContext
    {
        public DbSet<InventoryProduct> Products { get; set; }
        public DbSet<CategoryItem> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configures connection to local SQL Server instance using secure Windows auth parameters
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=EnterpriseInventoryDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional fluent configuration or seeding mapping rules
            modelBuilder.Entity<CategoryItem>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
