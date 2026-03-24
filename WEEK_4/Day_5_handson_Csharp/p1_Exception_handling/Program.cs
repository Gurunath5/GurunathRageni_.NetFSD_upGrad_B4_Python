/*Level-1 Problem 1: Safe Division Calculator
Scenario:
A calculator application must perform division operations safely. If the user enters an invalid value or tries to divide by zero, the system should handle the error gracefully instead of crashing.
Requirements:
1.Create a class Calculator.
2.Create a method Divide(int numerator, int denominator).
3.Use try-catch-finally blocks to handle errors.
4.If the denominator is zero, display an appropriate error message.
5.Ensure the program continues execution after handling the error.
Technical Constraints:
1.Use try-catch-finally blocks for exception handling.
2.Handle DivideByZeroException.
3.Ensure the finally block always executes.
Expectations:
Demonstrate proper use of try-catch-finally blocks.
Display meaningful error messages.
Ensure program stability even when errors occur.
Learning Outcome:
Understand basic exception handling.
Use try-catch-finally blocks effectively.
Handle runtime errors safely.
Sample Input:
Numerator = 20
 Denominator = 0
Sample Output:
Error: Cannot divide by zero
 Operation completed safe*/
using System;

namespace ExceptionHandlingDemo
{
    class Calculator
    {
        public void Divide(int numerator, int denominator)
        {
            try
            {
                int result = numerator / denominator;
                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Cannot divide by zero");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Operation completed safely");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Calculator calc = new Calculator();

            Console.Write("Enter Numerator: ");
            int num = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Denominator: ");
            int den = Convert.ToInt32(Console.ReadLine());

            calc.Divide(num, den);

            // Program continues after error
            Console.WriteLine("\nProgram is still running...");

            Console.ReadLine();
        }
    }
}