using System;

class StudentEnrollment
{
    // Required modifier: forces properties to be set on object initialization
    public required string StudentId { get; set; }
    public required string FullName { get; set; }
    public string Department { get; set; } = "General Science";
}

class Program
{
    static void Main()
    {
        // Must supply StudentId and FullName, otherwise compile error:
        var student = new StudentEnrollment
        {
            StudentId = "ST-9021",
            FullName = "David Miller"
        };
        Console.WriteLine($"Enrolled: {student.FullName} [{student.StudentId}]");
    }
}
