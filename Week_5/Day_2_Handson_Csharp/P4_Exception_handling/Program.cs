using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter root directory path: ");
            string path = Console.ReadLine();

            // Validate path
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid directory path.");
                return;
            }

            DirectoryInfo rootDir = new DirectoryInfo(path);

            // Get all subdirectories
            DirectoryInfo[] subDirs = rootDir.GetDirectories();

            if (subDirs.Length == 0)
            {
                Console.WriteLine("No subdirectories found.");
                return;
            }

            Console.WriteLine("\n===== Directory Analysis =====\n");

            foreach (DirectoryInfo dir in subDirs)
            {
                try
                {
                    // Count files in each directory
                    FileInfo[] files = dir.GetFiles();

                    Console.WriteLine("Folder: " + dir.Name);
                    Console.WriteLine("Number of Files: " + files.Length);
                    Console.WriteLine("-----------------------------");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Folder: " + dir.Name);
                    Console.WriteLine("Access Denied.");
                    Console.WriteLine("-----------------------------");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}