using System;

class Program
{
    // ref: passing variables by reference (read & write)
    static void DoubleValue(ref int number)
    {
        number *= 2;
    }

    // out: returning value via parameter (must be assigned inside)
    static void CalculateDivision(int dividend, int divisor, out int quotient, out int remainder)
    {
        quotient = dividend / divisor;
        remainder = dividend % divisor;
    }

    // in: passing variables as read-only reference (cannot modify)
    static void PrintPayload(in string data)
    {
        // data = "new val"; // Compilier error
        Console.WriteLine("Immutable payload: " + data);
    }

    static void Main()
    {
        int x = 25;
        DoubleValue(ref x);
        Console.WriteLine("Ref doubled: " + x);

        CalculateDivision(17, 5, out int q, out int r);
        Console.WriteLine($"Out results: Quotient = {q}, Remainder = {r}");

        string msg = "Secret transaction keys";
        PrintPayload(in msg);
    }
}
