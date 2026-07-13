using System;

// Record: immutable class configuration with value-based equality checking
public record StaffRecord(int EmployeeId, string FullName)
{
    // Init-only property
    public string Department { get; init; } = "General Operations";
}

class Program
{
    static void Main()
    {
        var s1 = new StaffRecord(101, "Liam O'Connor") { Department = "Engineering" };
        var s2 = new StaffRecord(101, "Liam O'Connor") { Department = "Engineering" };
        
        // Equality check by value (Records compare attributes rather than memory pointers)
        Console.WriteLine("s1 equals s2: " + (s1 == s2));

        // Attempting to modify properties results in compiler errors:
        // s1.Department = "QA"; // Compiler error: Init-only property cannot be mutated
    }
}
