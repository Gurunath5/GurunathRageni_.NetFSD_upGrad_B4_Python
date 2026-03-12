using System;
using System.Collections.Generic;
using System.Text;

namespace Day_3
{
    public class Student
    {
        public double CalculateAverage(int m1, int m2, int m3)
        {
            return ((m1 + m2 + m3) / 3.0);

        }


        public string GetGrade(double avg)
        {
            if (avg >= 90)
                return "A+";
            else if (avg >= 80)
                return "A";
            else if (avg >= 70)
                return "B";
            else if (avg >= 60)
                return "C";
            else
                return "Fail";
        }
    }
    internal class Program1
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter m1 marks:");
            int m1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter m2 marks:");
            int m2 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter m3 marks:");
            int m3 = Convert.ToInt32(Console.ReadLine());

            Student objStudent = new Student();

            double avg = objStudent.CalculateAverage(m1, m2, m3);
            String grade = objStudent.GetGrade(avg);
            Console.WriteLine($"average:{avg} Grade:{grade}");

        }
    }
}
