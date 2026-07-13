using System;

class InventoryItem
{
    private double _unitPrice; // Backing field

    public string SKU { get; set; } // Auto-implemented property

    public double UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value < 0) throw new ArgumentException("Price cannot be negative.");
            _unitPrice = value;
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            var item = new InventoryItem { SKU = "SKU-4920", UnitPrice = 45.99 };
            Console.WriteLine($"Item {item.SKU} costs {item.UnitPrice}");
            item.UnitPrice = -5.00; // Trigger validation error
        }
        catch (Exception ex)
        {
            Console.WriteLine("Validation Blocked: " + ex.Message);
        }
    }
}
