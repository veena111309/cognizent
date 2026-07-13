using System;
using System.Threading.Tasks;

class SecureCounter
{
    private readonly object _lockObject = new object();
    public int Count { get; private set; }

    public void UnsafeIncrement()
    {
        Count++; // Race condition when multiple threads execute concurrently
    }

    public void SafeIncrement()
    {
        lock (_lockObject)
        {
            Count++; // Thread-safe synchronization lock
        }
    }
}

class Program
{
    static async Task Main()
    {
        var counter = new SecureCounter();
        
        Console.WriteLine("Running safe thread pool increments...");
        Task[] tasks = new Task[100];
        for (int i = 0; i < 100; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    counter.SafeIncrement();
                }
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("Final safely synchronized count: " + counter.Count); // Outputs exactly 100,000
    }
}
