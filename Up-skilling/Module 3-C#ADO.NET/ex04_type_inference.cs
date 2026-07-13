using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Implicit typing via 'var' keyword
        var age = 25;
        var message = "Type inference compiler checks";
        var numbersList = new List<int> { 10, 20, 30 };

        Console.WriteLine($"Age type: {age.GetType()}");
        Console.WriteLine($"Message type: {message.GetType()}");
        Console.WriteLine($"List type: {numbersList.GetType()}");
    }
}
