using System;
using System.Web; // Requires System.Web reference

class Program
{
    static void Main()
    {
        // Malicious user-supplied payload string
        string dirtyInput = "<script>alert('Stealing cookie session keys!');</script>";

        // HTML Encoding resolves execution risks by escaping brackets
        string cleanOutput = HttpUtility.HtmlEncode(dirtyInput);

        Console.WriteLine("Original User Input: " + dirtyInput);
        Console.WriteLine("Encoded Safe Output: " + cleanOutput);
        // cleanOutput will be safely rendered as text: &lt;script&gt;alert('Stealing cookie session keys!');&lt;/script&gt;
    }
}
