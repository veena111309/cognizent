using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Fibonacci element 7: " + GetFibonacci(7));

        // Outer method containing local helper function
        int GetFibonacci(int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;

            // Local recursive function
            int Compute(int term)
            {
                if (term <= 1) return term;
                return Compute(term - 1) + Compute(term - 2);
            }

            return Compute(n);
        }
    }
}
