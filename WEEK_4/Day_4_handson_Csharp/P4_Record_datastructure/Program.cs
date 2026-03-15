/*Level-2 Problem 2: Student Record Management System Using Record Data Structure

Scenario:
A college wants to develop a console-based Student Record Management System. The system should store and manage student records using a Record data structure. Each student record should contain fields such as Roll Number, Name, Course, and Marks. The system must allow users to add multiple student records, display all records, and search for a student using the Roll Number.
Requirements:
1. Define a Record to store student details.
2. Allow the user to input details for multiple students.
3. Display all student records in a formatted manner.
4. Provide a search functionality to find a student by Roll Number.
5. Display appropriate message if the record is not found.
Technical Constraints:
1. Must use Record data type.
2. Use an array (or list) of records to store multiple students.
3. Do not use external databases.
4. Program should be menu-driven.
5. Input validation must be handled for Roll Number and Marks.

Sample Input:
Enter number of students: 2
Enter Roll Number: 101
Enter Name: Aisha
Enter Course: BCA
Enter Marks: 85

Enter Roll Number: 102
Enter Name: Rahul
Enter Course: BSc
Enter Marks: 78

Search Roll Number: 101

Sample Output:
Student Records:
Roll No: 101 | Name: Aisha | Course: BCA | Marks: 85
Roll No: 102 | Name: Rahul | Course: BSc | Marks: 78

Search Result:
Student Found:
Roll No: 101 | Name: Aisha | Course: BCA | Marks: 85

Expectations:
1. Proper use of Record/Structure syntax.
2. Clean and modular code.
3. Proper formatting of displayed output.
4. Efficient search implementation.
Learning Outcome:
1. Understand how to define and use Record/Structure data types.
2. Learn how to manage multiple records using arrays/lists.
3. Implement searching techniques on structured data.
4. Develop structured problem-solving skills for real-world scenarios.

*/

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