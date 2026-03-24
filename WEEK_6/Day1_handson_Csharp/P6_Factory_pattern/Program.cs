using System;

class Program
{
    static void Main(string[] args)
    {
        NotificationFactory factory = new NotificationFactory();

        var notification1 = factory.CreateNotification("email");
        notification1.Send("Welcome to our service!");

        var notification2 = factory.CreateNotification("sms");
        notification2.Send("Your OTP is 1234");

        var notification3 = factory.CreateNotification("push");
        notification3.Send("You have a new message!");

        Console.ReadLine();
    }
}