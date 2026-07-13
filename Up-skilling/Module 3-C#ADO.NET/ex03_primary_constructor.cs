using System;

// C# 12 Primary Constructor syntax on class
class UserProfile(string username, string email, int age)
{
    public string Username { get; } = username;
    public string Email { get; } = email;
    public int Age { get; } = age;

    public void PrintDetails()
    {
        Console.WriteLine($"User: {Username} | Contact: {Email} | Age: {Age}");
    }
}

class Program
{
    static void Main()
    {
        var profile = new UserProfile("veena_dev", "veena@example.com", 24);
        profile.PrintDetails();
    }
}
