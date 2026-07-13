using System;

class Program
{
    // Method returning multiple values via tuple
    static (int id, string title, double score) GetReport()
    {
        return (2041, "Quarterly Civic Review", 4.75);
    }

    static void Main()
    {
        // 1. Access by named fields
        var report = GetReport();
        Console.WriteLine($"ID: {report.id} | Name: {report.title} | Rating: {report.score}");

        // 2. Deconstruction syntax
        var (reportId, reportTitle, _) = GetReport();
        Console.WriteLine($"Deconstructed: ID={reportId}, Title={reportTitle}");
    }
}
