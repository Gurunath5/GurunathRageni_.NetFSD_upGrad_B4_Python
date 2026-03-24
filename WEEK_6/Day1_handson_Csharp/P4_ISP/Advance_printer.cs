using System;

public class AdvancedPrinter : IPrinter, IScanner, IFax
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string document)
    {
        Console.WriteLine("Scanning: " + document);
    }

    public void Fax(string document)
    {
        Console.WriteLine("Faxing: " + document);
    }
}