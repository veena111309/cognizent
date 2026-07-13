using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public string Department { get; set; }
}

class Program
{
    static void Main()
    {
        var staff = new List<Employee>
        {
            new Employee { Name = "Liam", Salary = 65000, Department = "IT" },
            new Employee { Name = "Sophia", Salary = 72000, Department = "IT" },
            new Employee { Name = "Emma", Salary = 55000, Department = "HR" }
        };

        // LINQ Method Syntax
        var highEarners = staff
            .Where(e => e.Salary > 60000 && e.Department == "IT")
            .Select(e => e.Name)
            .ToList();

        Console.WriteLine("High Earners: " + string.Join(", ", highEarners));
    }
}
