/*Level-2 Problem 3: Application Tracing for Order Processing
Scenario
An e-commerce application processes customer orders. Sometimes the order processing fails, but developers are unable to identify where the failure occurs. The team decides to implement Tracing to monitor the execution flow and diagnose the issue.
Requirements
Create a console application that simulates order processing.
The order processing should include the following steps:
1.Validate Order
2.Process Payment
3.Update Inventory
4.Generate Invoice
Use Trace class to log messages at each step of the process.
Display trace messages showing the execution flow.
Technical Constraints
Use System.Diagnostics.Trace.
Write trace messages using:
oTrace.WriteLine()
oTrace.TraceInformation()
Configure a TextWriterTraceListener to store trace logs in a file.
Implement the solution using .NET console application.
Expectations
The application should log messages for each stage of order processing.
Trace logs should help developers identify where failures occur.
The trace output should be stored in a log file.
Learning Outcome
Students will learn how to:
Implement application tracing using System.Diagnostics.
Monitor application flow using Trace listeners.
Use trace logs to diagnose runtime issues in real-world applications.*/

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Configure Trace to write to file
        Trace.Listeners.Clear();
        Trace.Listeners.Add(new TextWriterTraceListener("traceLog.txt"));
        Trace.AutoFlush = true;

        Trace.WriteLine("Application Started");

        try
        {
            await ProcessOrderAsync();
        }
        catch (Exception ex)
        {
            Trace.WriteLine("ERROR: " + ex.Message);
        }

        Trace.WriteLine("Application Ended");

        Console.WriteLine("Order processing completed. Check traceLog.txt");
        Console.ReadLine();
    }

    static async Task ProcessOrderAsync()
    {
        await ValidateOrderAsync();
        await ProcessPaymentAsync();
        await UpdateInventoryAsync();
        await GenerateInvoiceAsync();
    }

    static async Task ValidateOrderAsync()
    {
        Trace.TraceInformation("Step 1: Validating Order...");
        await Task.Delay(1000);
        Trace.WriteLine("Order validated successfully.");
    }

    static async Task ProcessPaymentAsync()
    {
        Trace.TraceInformation("Step 2: Processing Payment...");
        await Task.Delay(1500);

        // Simulate failure (for debugging demo)
        bool paymentSuccess = true;

        if (!paymentSuccess)
        {
            throw new Exception("Payment failed!");
        }

        Trace.WriteLine("Payment processed successfully.");
    }

    static async Task UpdateInventoryAsync()
    {
        Trace.TraceInformation("Step 3: Updating Inventory...");
        await Task.Delay(1200);
        Trace.WriteLine("Inventory updated successfully.");
    }

    static async Task GenerateInvoiceAsync()
    {
        Trace.TraceInformation("Step 4: Generating Invoice...");
        await Task.Delay(800);
        Trace.WriteLine("Invoice generated successfully.");
    }
}