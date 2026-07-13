using System;
using System.Threading.Tasks;

class Program
{
    // Async method representing file processing operations
    static async Task<int> ProcessFileAsync(string path)
    {
        Console.WriteLine($"[Async]: Starting processing on file {path}...");
        
        // Mock heavy I/O operation
        await Task.Delay(2000); 

        Console.WriteLine("[Async]: Processing completed.");
        return 1024; // Bytes processed
    }

    static async Task Main()
    {
        Console.WriteLine("Main thread running...");
        Task<int> processingTask = ProcessFileAsync("C:\\data\\roster.csv");
        
        Console.WriteLine("Doing concurrent operations in Main...");
        int size = await processingTask;

        Console.WriteLine($"Total processed size: {size} bytes.");
    }
}
