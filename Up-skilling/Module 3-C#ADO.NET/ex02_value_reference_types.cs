using System;

struct PointVal
{
    public int X;
    public int Y;
}

class PointRef
{
    public int X;
    public int Y;
}

class Program
{
    static void Main()
    {
        // Value type demonstration
        PointVal p1 = new PointVal { X = 10, Y = 20 };
        PointVal p2 = p1; // Copied by value
        p2.X = 99;
        Console.WriteLine($"p1.X: {p1.X} | p2.X: {p2.X} (Value Type copied data)");

        // Reference type demonstration
        PointRef r1 = new PointRef { X = 10, Y = 20 };
        PointRef r2 = r1; // Copied by reference pointer
        r2.X = 99;
        Console.WriteLine($"r1.X: {r1.X} | r2.X: {r2.X} (Reference Type pointed to same data)");
    }
}
