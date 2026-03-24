using System;

class Program
{
    static void Main(string[] args)
    {
        var config1 = ConfigurationManager.GetInstance();
        var config2 = ConfigurationManager.GetInstance();

        Console.WriteLine("Config 1:");
        PrintConfig(config1);

        Console.WriteLine("\nConfig 2:");
        PrintConfig(config2);

        // Check if both instances are same
        Console.WriteLine("\nAre both instances same? " + (config1 == config2));

        Console.ReadLine();
    }

    static void PrintConfig(ConfigurationManager config)
    {
        Console.WriteLine("App Name: " + config.ApplicationName);
        Console.WriteLine("Version: " + config.Version);
        Console.WriteLine("DB Connection: " + config.DatabaseConnectionString);
    }
}