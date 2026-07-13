using System;

class Vehicle {}
class Car : Vehicle { public int SeatCount { get; set; } = 5; }
class Truck : Vehicle { public double PayloadCapacity { get; set; } = 12.5; }

class Program
{
    static void Main()
    {
        Vehicle v1 = new Car();
        Vehicle v2 = new Truck();

        PrintVehicleSummary(v1);
        PrintVehicleSummary(v2);

        // Pattern matching switch expression
        double tollRate = GetTollFee(v1);
        Console.WriteLine("Toll fee for car: $" + tollRate);
    }

    static void PrintVehicleSummary(Vehicle v)
    {
        // Type pattern matching with local variable declaration
        if (v is Car c)
        {
            Console.WriteLine($"Vehicle is a Car with {c.SeatCount} seats.");
        }
        else if (v is Truck t)
        {
            Console.WriteLine($"Vehicle is a Truck with {t.PayloadCapacity} ton capacity.");
        }
    }

    static double GetTollFee(Vehicle v) => v switch
    {
        Car c when c.SeatCount > 7 => 12.50,
        Car _ => 6.00,
        Truck t when t.PayloadCapacity > 10 => 25.00,
        Truck _ => 15.00,
        null => throw new ArgumentNullException(nameof(v)),
        _ => 5.00
    };
}
