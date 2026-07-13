#nullable enable
using System;

class ContactDetails
{
    public string Name { get; set; } // Required non-nullable reference
    public string? EmailAddress { get; set; } // Optional nullable reference

    public ContactDetails(string name)
    {
        Name = name;
    }
}

class Program
{
    static void Main()
    {
        var contact = new ContactDetails(" Sophia");
        Console.WriteLine("Contact: " + contact.Name.Trim());

        // Compiler warns about potential null dereference if we use contact.EmailAddress.Length directly
        if (contact.EmailAddress != null)
        {
            Console.WriteLine("Email length: " + contact.EmailAddress.Length);
        }
    }
}
