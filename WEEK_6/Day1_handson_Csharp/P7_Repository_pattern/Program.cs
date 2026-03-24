using System;

class Program
{
    static void Main(string[] args)
    {
        IStudentRepository repository = new StudentRepository();

        // Add Students
        repository.AddStudent(new Student { StudentId = 1, StudentName = "Ravi", Course = "Python" });
        repository.AddStudent(new Student { StudentId = 2, StudentName = "Sita", Course = "Java" });
        repository.AddStudent(new Student { StudentId = 3, StudentName = "Arjun", Course = "C#" });

        // View All Students
        Console.WriteLine("All Students:");
        foreach (var student in repository.GetAllStudents())
        {
            Console.WriteLine($"{student.StudentId} - {student.StudentName} - {student.Course}");
        }

        Console.WriteLine("\nFind Student by ID (2):");
        var found = repository.GetStudentById(2);
        if (found != null)
            Console.WriteLine($"{found.StudentId} - {found.StudentName} - {found.Course}");

        // Delete Student
        Console.WriteLine("\nDeleting Student with ID 1...");
        repository.DeleteStudent(1);

        // View Again
        Console.WriteLine("\nUpdated Student List:");
        foreach (var student in repository.GetAllStudents())
        {
            Console.WriteLine($"{student.StudentId} - {student.StudentName} - {student.Course}");
        }

        Console.ReadLine();
    }
}