using System;

public class NotificationFactory
{
    public INotification CreateNotification(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type cannot be empty");

        switch (type.ToLower())
        {
            case "email":
                return new EmailNotification();

            case "sms":
                return new SMSNotification();

            case "push":
                return new PushNotification();

            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}