using System;

namespace StudentRecordSystem
{
    // Record Definition
    public record Student(int RollNumber, string Name, string Course, int Marks);

    class Program
    {
        static void DisplayStudents(Student[] students, int count)
        {
            Console.WriteLine("\nStudent Records:");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Roll No: {students[i].RollNumber} | Name: {students[i].Name} | Course: {students[i].Course} | Marks: {students[i].Marks}");
            }
        }

        static void SearchStudent(Student[] students, int count, int roll)
        {
            bool found = false;

            for (int i = 0; i < count; i++)
            {
                if (students[i].RollNumber == roll)
                {
                    Console.WriteLine("\nStudent Found:");
                    Console.WriteLine($"Roll No: {students[i].RollNumber} | Name: {students[i].Name} | Course: {students[i].Course} | Marks: {students[i].Marks}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Student record not found.");
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Enter number of students: ");
            int n = int.Parse(Console.ReadLine());

            Student[] students = new Student[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEnter details for student {i + 1}");

                Console.Write("Enter Roll Number: ");
                int roll = int.Parse(Console.ReadLine());

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Course: ");
                string course = Console.ReadLine();

                Console.Write("Enter Marks: ");
                int marks = int.Parse(Console.ReadLine());

                students[i] = new Student(roll, name, course, marks);
            }

            DisplayStudents(students, n);

            Console.Write("\nSearch Roll Number: ");
            int searchRoll = int.Parse(Console.ReadLine());

            SearchStudent(students, n, searchRoll);
        }
    }
}