using System;

class BaseEntity
{
    public string PublicField = "Public: Accessible everywhere";
    protected string ProtectedField = "Protected: Accessible in derived classes";
    private string PrivateField = "Private: Accessible only inside BaseEntity";
    internal string InternalField = "Internal: Accessible within same assembly";

    public void PrintPrivate() => Console.WriteLine(PrivateField);
}

class DerivedEntity : BaseEntity
{
    public void DisplayProtected()
    {
        Console.WriteLine(ProtectedField); // Works
        // Console.WriteLine(PrivateField); // Compiler error
    }
}

class Program
{
    static void Main()
    {
        var derived = new DerivedEntity();
        Console.WriteLine(derived.PublicField);
        Console.WriteLine(derived.InternalField);
        derived.DisplayProtected();
        derived.PrintPrivate();
    }
}
