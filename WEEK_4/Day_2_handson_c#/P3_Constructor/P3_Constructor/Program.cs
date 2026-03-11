using System;
using System.Collections.Generic;
using System.Text;

namespace Day_2
{
    class Calculator
    {
        private int a;
        private int b;

        // Constructor
        public Calculator(int x, int y)
        {
            this.a = x;
            this.b = y;
        }

        public int add()
        {
            return a + b;
        }

        public int subtract()
        {
            return a - b;
        }
    }

    internal class Problem3
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter First Number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Second Number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            Calculator calc_obj = new Calculator(num1, num2);


            Console.WriteLine("Addition: " + calc_obj.add());
            Console.WriteLine("Subtract: " + calc_obj.subtract());
        }
    }
}