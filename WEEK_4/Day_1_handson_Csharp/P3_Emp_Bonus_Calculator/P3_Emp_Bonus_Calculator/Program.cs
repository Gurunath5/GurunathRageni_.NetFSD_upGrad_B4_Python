/*Level-2 Problem 1: Employee Bonus Calculator
Scenario
Develop a console application that calculates employee bonus based on salary and years of experience.
Requirements
• Accept employee name, salary and years of experience.
• Use if-else and conditional operator.
• Bonus rules:
   - Experience < 2 years: 5% bonus
   - 2-5 years: 10% bonus
   - >5 years: 15% bonus
• Display final salary after bonus.
Technical Constraints
• Use double for salary.
• Use if-else and ternary operator.
• Use proper formatting for currency output.
Sample Input
Enter Name: Aisha
Enter Salary: 50000
Enter Experience: 4
Sample Output
Employee: Aisha
Bonus: 5000
Final Salary: 55000
Expectations
Accurate bonus calculation and correct usage of control statements.
Learning Outcome
Apply conditional logic and arithmetic operations in real-world scenarios.*/

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
