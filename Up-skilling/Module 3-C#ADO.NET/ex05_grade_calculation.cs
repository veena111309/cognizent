using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Score Percentage (0-100): ");
        if (int.TryParse(Console.ReadLine(), out int score))
        {
            char grade = score switch
            {
                >= 90 => 'A',
                >= 80 => 'B',
                >= 70 => 'C',
                >= 60 => 'D',
                _ => 'F'
            };
            Console.WriteLine($"Resulting Grade: {grade}");
        }
        else
        {
            Console.WriteLine("Invalid input. Numeric percentage required.");
        }
    }
}
