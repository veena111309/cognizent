using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string message = "MemoryStream bytes transfer payload.";
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        // MemoryStream: processing data buffer in memory
        using (MemoryStream ms = new MemoryStream(buffer))
        {
            using (StreamReader reader = new StreamReader(ms))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine("MemoryStream output: " + text);
            }
        }

        // FileStream: writing data to physical file
        string tempPath = Path.Combine(Path.GetTempPath(), "test_stream.txt");
        using (FileStream fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        {
            byte[] fileData = Encoding.UTF8.GetBytes("FileStream written data.");
            fs.Write(fileData, 0, fileData.Length);
        }
        Console.WriteLine("FileStream demo: Data successfully written to: " + tempPath);
    }
}
