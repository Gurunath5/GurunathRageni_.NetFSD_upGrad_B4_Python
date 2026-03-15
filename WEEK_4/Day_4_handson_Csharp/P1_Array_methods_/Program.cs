using System;
using System.Linq;
using System.Collections.Generic;

namespace StudentScoreAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Marks array
            int[] marks = { 78, 85, 90, 67, 88 };

            int threshold = 80;

            // Total marks (reduce equivalent)
            int totalMarks = marks.Sum();

            // Average marks
            double averageMarks = marks.Average();

            // Students above threshold (filter equivalent)
            var aboveThreshold = marks.Where(m => m > threshold);

            // Highest score
            int highestScore = marks.Max();

            // Dictionary for subject-wise highest marks
            Dictionary<string, int> subjectHighest = new Dictionary<string, int>();

            subjectHighest["Math"] = 90;
            subjectHighest["Science"] = 88;
            subjectHighest["English"] = 85;

            // Output
            Console.WriteLine("Total Marks: " + totalMarks);
            Console.WriteLine("Average Marks: " + averageMarks);
            Console.WriteLine("Students above 80: " + aboveThreshold.Count());
            Console.WriteLine("Highest Score: " + highestScore);

            Console.WriteLine("\nSubject Highest Marks:");

            foreach (var item in subjectHighest)
            {
                Console.WriteLine(item.Key + " : " + item.Value);
            }
        }
    }
}