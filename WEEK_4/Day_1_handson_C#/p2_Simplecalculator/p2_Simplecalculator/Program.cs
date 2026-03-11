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