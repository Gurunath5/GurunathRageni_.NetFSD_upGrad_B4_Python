using System;
using System.Collections.Generic;
using System.Text;

namespace Day_2
{
    public class calculator
    {
        private int a;
        private int b;
        public int add(int a, int b)
        {
            this.a = a;
            this.b = b;
            return a + b;
        }

        public int subtract(int a, int b)
        {
            this.a = a;
            this.b = b;
            return a + b;
        }
    }
    internal class Problem2
    {
        private int a;
        private int b;
        public int add(int a, int b)
        {
            this.a = a;
            this.b = b;
            return a + b;
        }

        public int subtract(int a, int b)
        {
            this.a = a;
            this.b = b;
            return a - b;
        }
        public static void Main(string[] args)
        {
            Console.Write("Enter First Number:");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Second Number:");
            int num2 = Convert.ToInt32(Console.ReadLine());



            calculator calc_obj = new calculator();

            int add_res = calc_obj.add(num1, num2);
            int sub_res = calc_obj.subtract(num1, num2);

            Console.WriteLine("Addition:" + add_res);
            Console.WriteLine("Subtract:" + sub_res);

        }
    }
}
