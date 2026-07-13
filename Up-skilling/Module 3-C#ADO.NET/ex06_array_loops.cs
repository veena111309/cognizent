using System;

class Program
{
    static void Main()
    {
        string[] departments = { "HR", "Engineering", "Marketing", "Finance" };

        Console.WriteLine("--- Iterating via For Loop ---");
        for (int i = 0; i < departments.Length; i++)
        {
            Console.WriteLine($"Dept [{i}]: {departments[i]}");
        }

        Console.WriteLine("\n--- Iterating via Foreach Loop ---");
        foreach (var dept in departments)
        {
            Console.WriteLine($"Dept: {dept}");
        }
    }
}
