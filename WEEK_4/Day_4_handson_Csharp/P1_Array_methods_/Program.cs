/*Level-1 Problem 1: Student Score Analyzer Using Arrays and Maps
Scenario:
A training institute wants to analyze student scores stored in an array. The system should calculate total marks, average, highest score, and count of students scoring above a threshold.
Requirements:
- Store student marks in an array.
- Use array methods (push, map, filter, reduce) for processing.
- Store subject-wise highest marks using a Map (key-value pair).
- Display total, average, and filtered results.
Technical Constraints:
- Must use array indexing and iteration.
- Use reduce() for total calculation.
- Use filter() for threshold-based filtering.
- Use Map or Dictionary for subject-highest mapping.
Sample Input:
Marks: [78, 85, 90, 67, 88]
Threshold: 80
Sample Output:
Total Marks: 408
Average Marks: 81.6
Students above 80: 3
Highest Score: 90
Expectations:
- Clean and modular implementation.
- Proper use of array methods.
- Efficient iteration logic.
Learning Outcome:
- Understand array manipulation.
- Use Map for key-value storage.
- Apply functional programming methods.*/

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