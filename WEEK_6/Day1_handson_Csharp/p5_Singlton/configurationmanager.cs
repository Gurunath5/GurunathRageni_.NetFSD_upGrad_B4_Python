using System;

public sealed class ConfigurationManager
{
    // Static instance (only one)
    private static ConfigurationManager _instance;

    // Lock object for thread safety
    private static readonly object _lock = new object();

    // Properties
    public string ApplicationName { get; private set; }
    public string Version { get; private set; }
    public string DatabaseConnectionString { get; private set; }

    // Private constructor (prevents new)
    private ConfigurationManager()
    {
        // Simulate loading config
        ApplicationName = "Inventory Management System";
        Version = "1.0.0";
        DatabaseConnectionString = "Server=localhost;Database=InventoryDB;Trusted_Connection=True;";
    }

    // Public method to get instance
    public static ConfigurationManager GetInstance()
    {
        // Double-check locking (thread-safe)
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
            }
        }
        return _instance;
    }
}