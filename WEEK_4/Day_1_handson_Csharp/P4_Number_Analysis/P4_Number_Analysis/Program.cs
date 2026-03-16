
/*Level-2 Problem 2: Number Analysis Using Loops
Scenario
Create a .NET 8 console application that analyzes numbers between 1 and N.
Requirements
• Accept a number N from user.
• Use loops to:
   - Count even numbers
   - Count odd numbers
   - Calculate sum of all numbers
• Display results.
Technical Constraints
• Use for or while loop.
• Use int data type.
• Avoid using arrays or collections.
Sample Input
Enter Number: 10
Sample Output
Even Count: 5
Odd Count: 5
Sum: 55
Expectations
Proper loop usage and correct counting logic.
Learning Outcome
Strengthen understanding of loops, counters and accumulators in C#.*/
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
