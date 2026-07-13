using System;

abstract class PaymentProcessor
{
    public string ProcessorName { get; }

    protected PaymentProcessor(string name) => ProcessorName = name;

    public abstract void ProcessCharge(double amount); // Must override in derived class
}

interface IInvoiceGenerator
{
    void GenerateInvoice(); // Contract interface
}

class StripeProcessor : PaymentProcessor, IInvoiceGenerator
{
    public StripeProcessor() : base("Stripe Gateway") {}

    public override void ProcessCharge(double amount)
    {
        Console.WriteLine($"Charging ${amount} via {ProcessorName}");
    }

    public void GenerateInvoice()
    {
        Console.WriteLine("Stripe Invoice HTML template compiled.");
    }
}

class Program
{
    static void Main()
    {
        StripeProcessor p = new StripeProcessor();
        p.ProcessCharge(150.00);
        p.GenerateInvoice();
    }
}
