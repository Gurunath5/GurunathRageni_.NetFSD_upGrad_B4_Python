using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter folder path: ");
            string folderPath = Console.ReadLine();

            // Validate directory
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Invalid directory path.");
                return;
            }

            // Get all files
            string[] files = Directory.GetFiles(folderPath);

            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the directory.");
                return;
            }

            Console.WriteLine("\nFiles in Directory:\n");

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                Console.WriteLine("File Name: " + fileInfo.Name);
                Console.WriteLine("File Size: " + fileInfo.Length + " bytes");
                Console.WriteLine("Created On: " + fileInfo.CreationTime);
                Console.WriteLine("----------------------------");
            }

            Console.WriteLine("\nTotal Files: " + files.Length);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access denied to the folder.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        Console.ReadLine();
    }
}