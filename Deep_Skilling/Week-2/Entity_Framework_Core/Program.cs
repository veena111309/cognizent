using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity_Framework_Core.Database;
using Entity_Framework_Core.Models;

namespace Entity_Framework_Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("====================================================================");
            Console.WriteLine("        Enterprise Entity Framework Core 8.0 Laboratory Runner       ");
            Console.WriteLine("====================================================================\n");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select a Laboratory Demo to Execute:");
                Console.WriteLine("1. Lab 1 - Understanding ORM Theory");
                Console.WriteLine("2. Lab 2 - Setup & Verify DbContext Schema");
                Console.WriteLine("3. Lab 3 - CLI Database Migrations Walkthrough");
                Console.WriteLine("4. Lab 4 - Insert Sample Data (Asynchronous)");
                Console.WriteLine("5. Lab 5 - Retrieve & Display Inventory Database");
                Console.WriteLine("6. Lab 6 - Update & Delete Records Operations");
                Console.WriteLine("7. Lab 7 - Execute LINQ Filters, OrderBy & DTO Projections");
                Console.WriteLine("8. Lab 8 - Model Schema Changes and Adjustments");
                Console.WriteLine("9. Lab 9 - Custom Data Seeding Concept Demo");
                Console.WriteLine("10. Execute All Labs Sequentially");
                Console.WriteLine("11. Exit");
                Console.Write("\nEnter Option (1-11): ");

                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunLab1();
                        break;
                    case "2":
                        RunLab2();
                        break;
                    case "3":
                        RunLab3();
                        break;
                    case "4":
                        await RunLab4();
                        break;
                    case "5":
                        await RunLab5();
                        break;
                    case "6":
                        await RunLab6();
                        break;
                    case "7":
                        await RunLab7();
                        break;
                    case "8":
                        RunLab8();
                        break;
                    case "9":
                        await RunLab9();
                        break;
                    case "10":
                        RunLab1();
                        RunLab2();
                        RunLab3();
                        await RunLab4();
                        await RunLab5();
                        await RunLab6();
                        await RunLab7();
                        RunLab8();
                        await RunLab9();
                        break;
                    case "11":
                        exit = true;
                        Console.WriteLine("Exiting EF Core Laboratory. Keep optimizing!");
                        break;
                    default:
                        Console.WriteLine("Invalid option selected. Please enter 1-11.");
                        break;
                }
                Console.WriteLine("\n--------------------------------------------------------------------\n");
            }
        }

        static void RunLab1()
        {
            Console.WriteLine("=== Lab 1: Understanding Object-Relational Mapping (ORM) ===");
            Console.WriteLine("- Concept: ORM maps application object representations to underlying SQL tables.");
            Console.WriteLine("- Benefits: Eliminates boilerplates, boosts security, and provides SQL query generation.");
            Console.WriteLine("- EF Core vs EF 6: EF Core is a lightweight, cross-platform rewritten engine.");
            Console.WriteLine("- EF Core 8 Features: Primitive collections, JSON column support, complex types.");
        }

        static void RunLab2()
        {
            Console.WriteLine("=== Lab 2: Setting Up the Database Context ===");
            using var context = new StoreDbContext();
            Console.WriteLine($"Database Context: {context.GetType().Name}");
            Console.WriteLine($"Category Set    : {context.Categories.EntityType.DisplayName()}");
            Console.WriteLine($"Product Set     : {context.Products.EntityType.DisplayName()}");
            Console.WriteLine("Configuration   : Microsoft SQL Server connection successfully mapped.");
        }

        static void RunLab3()
        {
            Console.WriteLine("=== Lab 3: EF Core Database Migrations ===");
            Console.WriteLine("Commands to apply migrations and sync your physical database:\n");
            Console.WriteLine("1. Install Global EF Core CLI Tool:");
            Console.WriteLine("   dotnet tool install --global dotnet-ef");
            Console.WriteLine("2. Create a Database Schema Migration:");
            Console.WriteLine("   dotnet ef migrations add InitialSystemCreate");
            Console.WriteLine("3. Apply Migrations to target DB Server:");
            Console.WriteLine("   dotnet ef database update");
        }

        static async Task RunLab4()
        {
            Console.WriteLine("=== Lab 4: Inserting Data (Async) ===");
            using var context = new StoreDbContext();
            
            // Check & ensure database is initialized (creates database if missing for testing)
            Console.WriteLine("Ensuring database schema exists...");
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine("Adding sample Categories...");
            var hardware = new CategoryItem { CategoryName = "Office Hardware" };
            var supplies = new CategoryItem { CategoryName = "Desk Supplies" };
            await context.Categories.AddRangeAsync(hardware, supplies);
            await context.SaveChangesAsync();

            Console.WriteLine("Adding sample Products...");
            var prod1 = new InventoryProduct
            {
                ProductName = "Precision Laser Printer",
                UnitPrice = 350.00m,
                StockLevel = 15,
                Category = hardware
            };
            var prod2 = new InventoryProduct
            {
                ProductName = "Ergonomic Mesh Chair",
                UnitPrice = 199.99m,
                StockLevel = 40,
                Category = supplies
            };
            var prod3 = new InventoryProduct
            {
                ProductName = "Mini Desk Stapler",
                UnitPrice = 12.50m,
                StockLevel = 120,
                Category = supplies
            };

            await context.Products.AddRangeAsync(prod1, prod2, prod3);
            await context.SaveChangesAsync();

            Console.WriteLine("Result: Insert operations complete and persisted!");
        }

        static async Task RunLab5()
        {
            Console.WriteLine("=== Lab 5: Retrieving & Displaying Database Records ===");
            using var context = new StoreDbContext();
            
            var products = await context.Products
                .Include(p => p.Category)
                .ToListAsync();

            if (products.Count == 0)
            {
                Console.WriteLine("No records found. Run Lab 4 first to populate.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-25} | {2,-15} | {3,-10} | {4,-10}", "ID", "Product Name", "Category", "Price", "Stock");
            Console.WriteLine(new string('-', 75));
            foreach (var p in products)
            {
                Console.WriteLine("{0,-5} | {1,-25} | {2,-15} | ${3,-9:F2} | {4,-10}", 
                    p.ProductId, p.ProductName, p.Category?.CategoryName ?? "None", p.UnitPrice, p.StockLevel);
            }
        }

        static async Task RunLab6()
        {
            Console.WriteLine("=== Lab 6: Updating & Deleting Database Records ===");
            using var context = new StoreDbContext();

            var targetProduct = await context.Products.FirstOrDefaultAsync(p => p.ProductName == "Mini Desk Stapler");
            if (targetProduct != null)
            {
                Console.WriteLine($"Found: {targetProduct.ProductName}. Old Price: ${targetProduct.UnitPrice}");
                targetProduct.UnitPrice = 14.99m; // Updating price
                await context.SaveChangesAsync();
                Console.WriteLine($"Update complete. New Price: ${targetProduct.UnitPrice}");
            }

            var deleteProduct = await context.Products.FirstOrDefaultAsync(p => p.ProductName == "Ergonomic Mesh Chair");
            if (deleteProduct != null)
            {
                Console.WriteLine($"Deleting: {deleteProduct.ProductName}...");
                context.Products.Remove(deleteProduct);
                await context.SaveChangesAsync();
                Console.WriteLine("Deletion completed successfully!");
            }
            else
            {
                Console.WriteLine("Ergonomic Mesh Chair not found for deletion. Run Lab 4 first.");
            }
        }

        static async Task RunLab7()
        {
            Console.WriteLine("=== Lab 7: LINQ Queries (Filters, Sort, & Projection) ===");
            using var context = new StoreDbContext();

            Console.WriteLine("\n--- Products priced above $100.00 (Ordered by descending price) ---");
            var premiumProducts = await context.Products
                .Where(p => p.UnitPrice > 100.00m)
                .OrderByDescending(p => p.UnitPrice)
                .ToListAsync();

            foreach (var p in premiumProducts)
            {
                Console.WriteLine($"* {p.ProductName} - ${p.UnitPrice}");
            }

            Console.WriteLine("\n--- DTO Projection (Fetching only Name and Price) ---");
            var projections = await context.Products
                .Select(p => new { p.ProductName, p.UnitPrice })
                .ToListAsync();

            foreach (var item in projections)
            {
                Console.WriteLine($"* Item: {item.ProductName} | Cost: ${item.UnitPrice}");
            }
        }

        static void RunLab8()
        {
            Console.WriteLine("=== Lab 8: Schema Changes ===");
            Console.WriteLine("To modify your database schema (e.g., adding properties like StockQuantity or Description):");
            Console.WriteLine("1. Declare the new C# property in your Entity models.");
            Console.WriteLine("2. Generate a delta migration:");
            Console.WriteLine("   dotnet ef migrations add AddStockLevelToProduct");
            Console.WriteLine("3. Apply the migration:");
            Console.WriteLine("   dotnet ef database update");
            Console.WriteLine("Entity Framework will generate ALTER TABLE SQL commands and migrate without losing existing records.");
        }

        static async Task RunLab9()
        {
            Console.WriteLine("=== Lab 9: Custom Data Seeding ===");
            Console.WriteLine("Demonstrating model configuration seeding...");
            using var context = new StoreDbContext();
            
            // Check database and print details of existing categories
            var counts = await context.Categories.CountAsync();
            Console.WriteLine($"Current seeded category records count: {counts}");
            Console.WriteLine("Note: In EF Core, seeding can be declared inside 'OnModelCreating' using 'HasData()' methods.");
        }
    }
}
