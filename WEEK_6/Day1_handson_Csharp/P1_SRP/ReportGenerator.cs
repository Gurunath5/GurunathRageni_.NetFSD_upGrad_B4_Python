using System;
using System.Collections.Generic;
public class ReportGenerator
{
    public void GenerateReport(List<Student> students)
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student data available.");
            return;
        }
        Console.WriteLine("===== Student Report =====");
        foreach (var student in students)
        {
            string grade = GetGrade(student.Marks);
            Console.WriteLine($"ID: {student.StudentId}");
            Console.WriteLine($"Name: {student.StudentName}");
            Console.WriteLine($"Marks: {student.Marks}");
            Console.WriteLine($"Grade: {grade}");
            Console.WriteLine("--------------------------");
        }
    }
    private string GetGrade(int marks)
    {
        if (marks >= 90) return "A";
        if (marks >= 75) return "B";
        if (marks >= 50) return "C";
        return "Fail";
    }
}