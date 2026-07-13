using System;

class Logger
{
    public void WriteLog(string message)
    {
        Console.WriteLine($"[INFO]: {message}");
    }

    public void WriteLog(string message, string level)
    {
        Console.WriteLine($"[{level.ToUpper()}]: {message}");
    }

    public void WriteLog(Exception ex)
    {
        Console.WriteLine($"[FATAL ERROR]: {ex.Message}");
    }
}

class Program
{
    static void Main()
    {
        var log = new Logger();
        log.WriteLog("System boot complete.");
        log.WriteLog("Database timeout warning.", "warn");
        log.WriteLog(new InvalidOperationException("Connection pool depleted."));
    }
}
