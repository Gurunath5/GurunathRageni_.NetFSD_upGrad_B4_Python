/*Level-1 Problem 2: Simple Calculator Using Switch
Scenario
Create a simple calculator application that performs basic arithmetic operations.
Requirements
• Accept two numbers from user.
• Accept operator (+, -, *, /).
• Use switch statement to perform operation.
• Display result.
Technical Constraints
• Use int or double data types.
• Use switch-case statement.
• Handle division by zero.
Sample Input
Enter First Number: 10
Enter Second Number: 5
Enter Operator: *
Sample Output
Result: 50
Expectations
Correct operator selection and proper validation of inputs.
Learning Outcome
Understand switch statements, arithmetic operators and control flow in C#.*/
using System;

namespace Day_1
{
    internal class Problem2
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter First Number: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Second Number: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Operator (+, -, *, /): ");
            char symbol = Convert.ToChar(Console.ReadLine());

            double result = 0;

            switch (symbol)
            {
                case '+':
                    result = num1 + num2;
                    Console.WriteLine("Result: " + result);
                    break;

                case '-':
                    result = num1 - num2;
                    Console.WriteLine("Result: " + result);
                    break;

                case '*':
                    result = num1 * num2;
                    Console.WriteLine("Result: " + result);
                    break;

                case '/':
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                    }
                    else
                    {
                        result = num1 / num2;
                        Console.WriteLine("Result: " + result);
                    }
                    break;

                default:
                    Console.WriteLine("Invalid Operator!");
                    break;
            }
        }
    }
}