using System;
using System.Collections.Generic;
using System.Linq;

public class StudentRepository : IStudentRepository
{
    private readonly List<Student> students = new List<Student>();

    public void AddStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        students.Add(student);
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Student GetStudentById(int id)
    {
        return students.FirstOrDefault(s => s.StudentId == id);
    }

    public void DeleteStudent(int id)
    {
        var student = GetStudentById(id);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        students.Remove(student);
    }
}