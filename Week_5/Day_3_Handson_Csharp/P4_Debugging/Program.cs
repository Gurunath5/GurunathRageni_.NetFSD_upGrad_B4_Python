/*
Level-2 Problem 2: Debugging Incorrect Discount Calculation
Scenario
A retail application calculates the final price of products after applying a discount. Recently, users reported that the final price shown by the application is incorrect. The development team needs to debug the application to identify where the incorrect calculation is happening.
Requirements
Create a console application that calculates the final product price.
The program should accept:
oProduct Name
oProduct Price
oDiscount Percentage
The final price should be calculated using the formula:
FinalPrice = Price − (Price × Discount / 100)
Use debugging tools to verify that the calculation is correct.
Place breakpoints and inspect variable values during execution.
Technical Constraints
Use Visual Studio Debugging Tools.
Use breakpoints, step over, step into, and watch window.
Implement the solution using a .NET console application.
Expectations
Students should run the program in debug mode.
They should track variable values and confirm that the discount calculation is correct.
If incorrect results appear, students should identify the faulty logic.
Learning Outcome
Students will learn how to:
Use breakpoints effectively.
Inspect variable values during program execution.
Identify logical errors using debugging techniques.*/

using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.Write("Enter Discount Percentage: ");
            if (!double.TryParse(Console.ReadLine(), out double discount) || discount < 0)
            {
                Console.WriteLine("Invalid discount.");
                return;
            }

            double finalPrice = CalculateFinalPrice(price, discount);

            Console.WriteLine("\n===== BILL =====");
            Console.WriteLine("Product: " + name);
            Console.WriteLine("Price: " + price);
            Console.WriteLine("Discount: " + discount + "%");
            Console.WriteLine("Final Price: " + finalPrice);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }

    static double CalculateFinalPrice(double price, double discount)
    {
        double discountAmount = price * discount / 100;
        double finalPrice = price - discountAmount;
        return finalPrice;
    }
}