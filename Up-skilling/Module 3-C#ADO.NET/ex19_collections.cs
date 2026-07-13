using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 1. List
        List<string> logs = new() { "Log A", "Log B" };
        logs.Add("Log C");

        // 2. Dictionary
        Dictionary<string, string> appSettings = new()
        {
            { "Env", "Development" },
            { "Port", "5001" }
        };

        // 3. Queue (FIFO)
        Queue<int> requests = new();
        requests.Enqueue(101);
        requests.Enqueue(102);

        // 4. Stack (LIFO)
        Stack<string> pageHistory = new();
        pageHistory.Push("Home");
        pageHistory.Push("Settings");

        Console.WriteLine($"Queue size: {requests.Count} | Stack top: {pageHistory.Peek()}");
    }
}
