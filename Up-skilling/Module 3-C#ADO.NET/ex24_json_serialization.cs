using System;
using System.Text.Json;

class ConfigurationSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; }
    public bool EnableCaching { get; set; }
}

class Program
{
    static void Main()
    {
        var settings = new ConfigurationSettings
        {
            ConnectionString = "Server=localhost;Database=VaultDB;",
            TimeoutSeconds = 30,
            EnableCaching = true
        };

        // Serialize Object to JSON
        string jsonOutput = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("--- Serialized JSON ---");
        Console.WriteLine(jsonOutput);

        // Deserialize JSON to Object
        var parsedSettings = JsonSerializer.Deserialize<ConfigurationSettings>(jsonOutput);
        Console.WriteLine($"\nParsed Timeout: {parsedSettings?.TimeoutSeconds} seconds");
    }
}
