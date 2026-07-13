package Algorithms_DataStructures.Exercise1;

import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import java.util.Scanner;

/**
 * ============================================================================
 * Inventory Management System
 * Goal: Track warehousing products efficiently using a Map data structure.
 * Scope: Deep Skilling Week 1 - Algorithms & Data Structures (Exercise 1)
 *
 * Data Structure Choice: HashMap<String, ProductItem>
 * Rationale:
 * - Key: Unique Product Code (String)
 * - Value: ProductItem object containing product metadata.
 * - HashMap provides O(1) average time complexity for insertion, deletion, 
 *   lookup, and updates, which is essential for massive scale warehouses.
 *
 * Time Complexity Analysis:
 * ----------------------------------------
 * | Operation        | HashMap Average | ArrayList Average |
 * |------------------|-----------------|-------------------|
 * | Add Product      | O(1)            | O(1) / O(n)       |
 * | Update Product   | O(1)            | O(n)              |
 * | Delete Product   | O(1)            | O(n)              |
 * | Search/Retrieve  | O(1)            | O(n)              |
 * | List/Display All | O(n)            | O(n)              |
 * ----------------------------------------
 * ============================================================================
 */

class ProductItem {
    private final String itemCode;
    private String name;
    private int stockCount;
    private double unitPrice;

    public ProductItem(String itemCode, String name, int stockCount, double unitPrice) {
        this.itemCode = itemCode;
        this.name = name;
        this.stockCount = stockCount;
        this.unitPrice = unitPrice;
    }

    // Getters and Setters with validation
    public String getItemCode() {
        return itemCode;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getStockCount() {
        return stockCount;
    }

    public void setStockCount(int stockCount) {
        if (stockCount < 0) {
            throw new IllegalArgumentException("Stock count cannot be negative.");
        }
        this.stockCount = stockCount;
    }

    public double getUnitPrice() {
        return unitPrice;
    }

    public void setUnitPrice(double unitPrice) {
        if (unitPrice < 0) {
            throw new IllegalArgumentException("Unit price cannot be negative.");
        }
        this.unitPrice = unitPrice;
    }

    @Override
    public String toString() {
        return String.format("Code: %-10s | Name: %-20s | Stock: %-5d | Price: $%.2f", 
                itemCode, name, stockCount, unitPrice);
    }
}

class InventoryRepository {
    private final Map<String, ProductItem> items = new HashMap<>();

    public void upsertProduct(ProductItem item) {
        items.put(item.getItemCode(), item);
    }

    public Optional<ProductItem> findProduct(String itemCode) {
        return Optional.ofNullable(items.get(itemCode));
    }

    public boolean removeProduct(String itemCode) {
        return items.remove(itemCode) != null;
    }

    public Map<String, ProductItem> getAllProducts() {
        return new HashMap<>(items); // returns defensive copy
    }

    public boolean isEmpty() {
        return items.isEmpty();
    }
}

public class InventoryManager {
    private static final InventoryRepository repo = new InventoryRepository();
    private static final Scanner scanner = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("=== Modern Warehouse Inventory Manager ===");
        boolean running = true;

        while (running) {
            System.out.println("\nSelect Action:");
            System.out.println("1. Add/Register Product");
            System.out.println("2. Update Product Stock/Price");
            System.out.println("3. Search Product Details");
            System.out.println("4. Delete Product from System");
            System.out.println("5. List All Warehouse Inventory");
            System.out.println("6. Exit");
            System.out.print("Choice: ");

            String choiceStr = scanner.nextLine().trim();
            switch (choiceStr) {
                case "1":
                    registerItem();
                    break;
                case "2":
                    updateItem();
                    break;
                case "3":
                    searchItem();
                    break;
                case "4":
                    deleteItem();
                    break;
                case "5":
                    displayInventory();
                    break;
                case "6":
                    running = false;
                    System.out.println("Exiting Warehouse System. Safe travels!");
                    break;
                default:
                    System.out.println("Invalid selection. Try again.");
            }
        }
        scanner.close();
    }

    private static void registerItem() {
        try {
            System.out.print("Enter Unique Item Code: ");
            String code = scanner.nextLine().trim();
            if (code.isEmpty()) throw new IllegalArgumentException("Code cannot be empty.");

            if (repo.findProduct(code).isPresent()) {
                System.out.println("Warning: Product with this code already exists. Use update option.");
                return;
            }

            System.out.print("Enter Product Name: ");
            String name = scanner.nextLine().trim();

            System.out.print("Enter Initial Stock Level: ");
            int stock = Integer.parseInt(scanner.nextLine().trim());

            System.out.print("Enter Unit Price ($): ");
            double price = Double.parseDouble(scanner.nextLine().trim());

            ProductItem item = new ProductItem(code, name, stock, price);
            repo.upsertProduct(item);
            System.out.println("Product successfully registered!");
        } catch (NumberFormatException e) {
            System.out.println("Error: Invalid numeric input.");
        } catch (IllegalArgumentException e) {
            System.out.println("Error: " + e.getMessage());
        }
    }

    private static void updateItem() {
        try {
            System.out.print("Enter Item Code to update: ");
            String code = scanner.nextLine().trim();

            Optional<ProductItem> optItem = repo.findProduct(code);
            if (optItem.isEmpty()) {
                System.out.println("Product not found in database.");
                return;
            }

            ProductItem item = optItem.get();
            System.out.print("Enter New Stock Count (or press Enter to skip): ");
            String stockInput = scanner.nextLine().trim();
            if (!stockInput.isEmpty()) {
                item.setStockCount(Integer.parseInt(stockInput));
            }

            System.out.print("Enter New Unit Price (or press Enter to skip): ");
            String priceInput = scanner.nextLine().trim();
            if (!priceInput.isEmpty()) {
                item.setUnitPrice(Double.parseDouble(priceInput));
            }

            System.out.println("Product details updated successfully!");
        } catch (NumberFormatException e) {
            System.out.println("Error: Invalid numeric formatting.");
        } catch (IllegalArgumentException e) {
            System.out.println("Error: " + e.getMessage());
        }
    }

    private static void searchItem() {
        System.out.print("Enter Item Code: ");
        String code = scanner.nextLine().trim();

        repo.findProduct(code).ifPresentOrElse(
            item -> {
                System.out.println("\nProduct Info Found:");
                System.out.println("Code         : " + item.getItemCode());
                System.out.println("Description  : " + item.getName());
                System.out.println("Current Stock: " + item.getStockCount());
                System.out.println("Price USD    : $" + item.getUnitPrice());
            },
            () -> System.out.println("Item not found.")
        );
    }

    private static void deleteItem() {
        System.out.print("Enter Item Code to delete: ");
        String code = scanner.nextLine().trim();

        if (repo.removeProduct(code)) {
            System.out.println("Product removed from index.");
        } else {
            System.out.println("Product not found.");
        }
    }

    private static void displayInventory() {
        if (repo.isEmpty()) {
            System.out.println("Inventory ledger is completely empty.");
            return;
        }

        System.out.println("\n--- Current Inventory Ledger ---");
        repo.getAllProducts().values().forEach(System.out.println);
    }
}
