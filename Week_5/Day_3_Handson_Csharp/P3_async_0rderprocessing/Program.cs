/*Level-2 Problem 1: Asynchronous Order Processing System
Scenario:
An e-commerce system processes customer orders. Each order requires multiple steps such as payment verification, inventory check, and order confirmation. These steps involve delays and should be handled asynchronously.

Requirements:
- Create asynchronous methods:
  - VerifyPaymentAsync()
  - CheckInventoryAsync()
  - ConfirmOrderAsync()
- Simulate processing delays using Task.Delay().
- Execute steps asynchronously while maintaining the logical order of operations.

Technical Constraints:
- Use async and await.
- Use Task-based asynchronous methods.
- Implement using a console application.

Expectations:
- Each processing step should run asynchronously.
- The program should display messages indicating the progress of order processing.

Learning Outcome:
Students will understand how to design real-world workflows using asynchronous methods with async/await.
*/
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Order processing started...\n");

        await ProcessOrderAsync();

        Console.WriteLine("\nOrder processing completed!");
        Console.ReadLine();
    }

    static async Task ProcessOrderAsync()
    {
        bool paymentStatus = await VerifyPaymentAsync();

        if (!paymentStatus)
        {
            Console.WriteLine("Payment failed. Order cancelled.");
            return;
        }

        bool inventoryStatus = await CheckInventoryAsync();

        if (!inventoryStatus)
        {
            Console.WriteLine("Out of stock. Order cancelled.");
            return;
        }

        await ConfirmOrderAsync();
    }

    static async Task<bool> VerifyPaymentAsync()
    {
        Console.WriteLine("Verifying payment...");
        await Task.Delay(2000); // simulate delay
        Console.WriteLine("Payment verified!");
        return true;
    }

    static async Task<bool> CheckInventoryAsync()
    {
        Console.WriteLine("Checking inventory...");
        await Task.Delay(3000); // simulate delay
        Console.WriteLine("Inventory available!");
        return true;
    }

    static async Task ConfirmOrderAsync()
    {
        Console.WriteLine("Confirming order...");
        await Task.Delay(1500); // simulate delay
        Console.WriteLine("Order confirmed!");
    }
}