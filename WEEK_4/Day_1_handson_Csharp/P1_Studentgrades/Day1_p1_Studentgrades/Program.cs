using System;
using System.Collections.Generic;
using System.Text;

namespace Day_1
{
    internal class Problem1
    {
        public static void Main(string[] args)
        {

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            if (name == "")
            {
                Console.WriteLine("enter valid name");
                return;
            }
            Console.Write("Enter Marks: ");
            int marks = Convert.ToInt32(Console.ReadLine());
            string grade = "";

            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Invalid Marks!");
                return;
            }
            else if (marks >= 90)
            {
                grade = "A";
            }
            else if (marks >= 75)
            {
                grade = "B";
            }
            else if (marks >= 60)
            {
                grade = "C";
            }
            else if (marks >= 40)
            {
                grade = "D";
            }
            else
            {
                grade = "Fail";
            }
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: " + grade);
        }
    }
}
