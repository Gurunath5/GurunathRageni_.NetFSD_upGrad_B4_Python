using System;

class Program
{
    static void Main(string[] args)
    {
        // Basic Printer
        IPrinter basicPrinter = new BasicPrinter();
        basicPrinter.Print("Hello World");

        Console.WriteLine("------------------");

        // Advanced Printer
        AdvancedPrinter advancedPrinter = new AdvancedPrinter();

        advancedPrinter.Print("Report");
        advancedPrinter.Scan("Document.pdf");
        advancedPrinter.Fax("Invoice.pdf");

        Console.ReadLine();
    }
}