using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly object ResourceA = new object();
    private static readonly object ResourceB = new object();

    static void Main()
    {
        // Demonstrating how to resolve deadlock by enforcing lock order
        Task t1 = Task.Run(() => SafeAcquire(1));
        Task t2 = Task.Run(() => SafeAcquire(2));

        Task.WaitAll(t1, t2);
        Console.WriteLine("Execution completed without deadlocking.");
    }

    static void SafeAcquire(int taskId)
    {
        // Enforce same locking hierarchy across all threads: Lock A then Lock B
        lock (ResourceA)
        {
            Console.WriteLine($"Task {taskId} acquired Resource A");
            Thread.Sleep(500); // Wait for potential deadlock condition

            lock (ResourceB)
            {
                Console.WriteLine($"Task {taskId} acquired Resource B");
            }
        }
    }
}
