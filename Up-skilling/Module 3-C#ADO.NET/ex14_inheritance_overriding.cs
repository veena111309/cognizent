using System;

class BaseService
{
    public virtual void Initialize()
    {
        Console.WriteLine("Standard services setup completed.");
    }
}

class DatabaseService : BaseService
{
    public override void Initialize()
    {
        base.Initialize(); // Runs base implementation
        Console.WriteLine("Database connection pool established.");
    }
}

class Program
{
    static void Main()
    {
        BaseService srv = new DatabaseService();
        srv.Initialize(); // Executes overriden method dynamically (Polymorphism)
    }
}
