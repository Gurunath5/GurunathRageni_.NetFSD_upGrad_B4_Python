using System;
using System.Collections.Generic;
using System.Text;

namespace Day_1
{
    internal class Problem3
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter name:");
            string name = Console.ReadLine();
            Console.Write("Enter salary:");
            double salary = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Exp:");
            int exp = Convert.ToInt32(Console.ReadLine());

            double bonus = 0;
            bonus = exp < 2 ? (salary / 100) * 5 : 0;
            if (exp >= 2 && exp <= 5)
            {
                bonus = (salary / 100) * 10;
            }
            else if (exp > 5)
            {
                bonus = (salary / 100) * 15;
            }

            Console.WriteLine($"Name:{name}");
            Console.WriteLine($"Bonus:{bonus}");
            Console.WriteLine($"Final Salary:{salary + bonus}");

        }
    }
}
