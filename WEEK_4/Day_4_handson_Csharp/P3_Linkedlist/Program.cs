/*Level-2 Problem 1: Employee Management Using Linked List
Scenario:
A company wants to maintain employee records dynamically using a Linked List structure.
Requirements:
- Create Node structure with employee ID and name.
- Implement insertion at beginning and end.
- Implement deletion by employee ID.
- Traverse and display employee list.
Technical Constraints:
- Must implement singly linked list.
- No use of built-in list structures.
- Proper memory handling and pointer updates.
Sample Input:
Insert: (101, John), (102, Sara), (103, Mike)
Delete: 102
Sample Output:
Employee List After Deletion:
101 - John
103 – Mike


Expectations:
- Correct node linking.
- Efficient traversal logic.
- Clean insertion and deletion operations.
Learning Outcome:
- Understand linked list structure.
- Perform insertion and deletion operations.
- Learn dynamic data structure behavior.*/

using System;

class Node
{
    public int empId;
    public string name;
    public Node next;

    public Node(int id, string name)
    {
        this.empId = id;
        this.name = name;
        this.next = null;
    }
}
class EmployeeLinkedList
{
    Node head;

    // Insert at Beginning
    public void InsertAtBeginning(int id, string name)
    {
        Node newNode = new Node(id, name);
        newNode.next = head;
        head = newNode;
    }

    // Insert at End
    public void InsertAtEnd(int id, string name)
    {
        Node newNode = new Node(id, name);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Node temp = head;

        while (temp.next != null)
        {
            temp = temp.next;
        }

        temp.next = newNode;
    }

    // Delete by Employee ID
    public void Delete(int id)
    {
        if (head == null)
            return;

        // If head node needs deletion
        if (head.empId == id)
        {
            head = head.next;
            return;
        }

        Node temp = head;

        while (temp.next != null && temp.next.empId != id)
        {
            temp = temp.next;
        }

        if (temp.next != null)
        {
            temp.next = temp.next.next;
        }
    }

    // Display Employees
    public void Display()
    {
        Node temp = head;

        while (temp != null)
        {
            Console.WriteLine(temp.empId + " - " + temp.name);
            temp = temp.next;
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        EmployeeLinkedList list = new EmployeeLinkedList();

        // Insert Employees
        list.InsertAtEnd(101, "John");
        list.InsertAtEnd(102, "Sara");
        list.InsertAtEnd(103, "Mike");

        // Delete employee with ID 102
        list.Delete(102);

        Console.WriteLine("Employee List After Deletion:");

        // Display list
        list.Display();
    }
}