using System;

class AccountHolder
{
    public string Name { get; set; }
    public string AccountType { get; set; }

    // Constructor A (Chained default)
    public AccountHolder(string name) : this(name, "Checking")
    {
    }

    // Constructor B (Primary target)
    public AccountHolder(string name, string accountType)
    {
        Name = name;
        AccountType = accountType;
    }

    public void Display() => Console.WriteLine($"Name: {Name} | Type: {AccountType}");
}

class Program
{
    static void Main()
    {
        var u1 = new AccountHolder("Alice");
        var u2 = new AccountHolder("Bob", "Savings");
        u1.Display();
        u2.Display();
    }
}
