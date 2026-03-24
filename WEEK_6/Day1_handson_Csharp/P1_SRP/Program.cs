
/*Problem: 1- SRP – Single Responsibility Principle
Scenario: Student Report Generator
A training institute like Codempower Academy maintains student information and generates performance reports. Currently, one class performs student data storage and report generation, which makes the code difficult to maintain.
Requirements:
1.Create a Student class with properties:
StudentId
StudentName
Marks
2.Create a class responsible for managing student data.
3.Create a separate class responsible for generating reports.
 4.The report should display:

Security Requirements (Secure Coding Practices)
Students must implement the following security measures:
Technical Constraints:
Use C# (.NET Console Application).
Each class must have only one responsibility.
Do not mix data storage and report generation logic in the same class.
Expectations:
Students should implement at least three classes:
Student
StudentRepository
ReportGenerator*/

using System;

class Program
{
    static void Main(string[] args)
    {
        StudentRepository repo = new StudentRepository();
        ReportGenerator reportGenerator = new ReportGenerator();

        // Adding students
        repo.AddStudent(new Student { StudentId = 1, StudentName = "Ravi", Marks = 85 });
        repo.AddStudent(new Student { StudentId = 2, StudentName = "Sita", Marks = 92 });
        repo.AddStudent(new Student { StudentId = 3, StudentName = "Arjun", Marks = 45 });

        // Generate report
        var students = repo.GetAllStudents();
        reportGenerator.GenerateReport(students);

        Console.ReadLine();
    }
}