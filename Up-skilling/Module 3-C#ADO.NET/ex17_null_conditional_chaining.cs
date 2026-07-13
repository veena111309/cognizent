#nullable enable
using System;

class AddressInfo
{
    public string City { get; set; } = "Unknown";
}

class UserProfile
{
    public AddressInfo? Address { get; set; }
}

class Program
{
    static void Main()
    {
        UserProfile? profile = null;

        // Null-conditional chaining prevents NullReferenceException
        string city = profile?.Address?.City ?? "Default City";
        Console.WriteLine("Profile City: " + city);

        profile = new UserProfile { Address = new AddressInfo { City = "San Francisco" } };
        city = profile?.Address?.City ?? "Default City";
        Console.WriteLine("Profile City: " + city);
    }
}
