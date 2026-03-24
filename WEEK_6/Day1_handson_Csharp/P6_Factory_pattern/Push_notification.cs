using System;

public class PushNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Push Notification Sent: " + message);
    }
}