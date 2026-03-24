using System;
using System.Collections.Generic;

public class StudentRepository
{
    private List<Student> students = new List<Student>();

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
}