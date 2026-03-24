using System;

public class BasicPrinter : IPrinter
{
    public void Print(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty");

        Console.WriteLine("Printing: " + content);
    }
}