using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Configure Trace Listeners
        Trace.Listeners.Add(new ConsoleTraceListener());
        
        Console.WriteLine("--- System Diagnostics Trace Output ---");
        
        Trace.WriteLine("Diagnostic Log: Tracking application initialize sequence.");
        Trace.WriteLineIf(true, "Diagnostic Log: System configurations validated.");
        
        // Flushes buffers to listener outputs
        Trace.Flush(); 
    }
}
