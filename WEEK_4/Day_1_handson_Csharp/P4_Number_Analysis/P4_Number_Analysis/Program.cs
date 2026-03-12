using System;
using System.Collections.Generic;
using System.Text;

namespace Day_1
{
    internal class Problem4
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter Number");
            int num = Convert.ToInt32(Console.ReadLine());
            int ecount = 0;
            int ocount = 0;
            int total = 0;
            for (int i = 1; i <= num; i++)
            {
                if (i % 2 == 0)
                {
                    ecount += 1;
                }
                else if (i % 2 != 0)
                {
                    ocount += 1;
                }
                total += i;
            }
            Console.WriteLine("Even Count:" + ecount);
            Console.WriteLine("Odd Count:" + ocount);
            Console.WriteLine("Total:" + total);
        }
    }
}
