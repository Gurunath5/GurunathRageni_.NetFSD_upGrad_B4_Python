using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "log.txt";

        try
        {
            Console.Write("Enter your message: ");
            string message = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Message cannot be empty.");
                return;
            }

            // Convert message to bytes
            byte[] data = Encoding.UTF8.GetBytes(message + Environment.NewLine);

            // Open file in Append mode
            using (FileStream fs = new FileStream(
                filePath,
                FileMode.Append,
                FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }

            Console.WriteLine("Message successfully written to file!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No permission to access the file.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: Directory not found.");
        }
        catch (IOException ex)
        {
            Console.WriteLine("File error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        Console.ReadLine();
    }
}