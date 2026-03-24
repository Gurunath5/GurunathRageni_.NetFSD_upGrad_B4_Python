using System.Collections.Generic;

public interface IStudentRepository
{
    void AddStudent(Student student);
    List<Student> GetAllStudents();
    Student GetStudentById(int id);
    void DeleteStudent(int id);
}