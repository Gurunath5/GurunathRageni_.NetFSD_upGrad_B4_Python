/*Level-2 Problem 3: Employee Salary Calculator
Scenario:
A company wants to calculate employee salaries using inheritance. All employees have a base salary, but different roles calculate bonuses differently.
Requirements:
1. Create a base class Employee with Name and BaseSalary properties.
2. Create derived classes Manager and Developer.
3. Override a virtual method CalculateSalary().
4. Manager gets 20% bonus, Developer gets 10% bonus.
Technical Constraints:
• Use inheritance and method overriding.
• Apply polymorphism using base class reference.
• Use properties for salary details.
Expectations:
• Demonstrate runtime polymorphism.
• Avoid code duplication.
• Display final calculated salary.
Learning Outcome:
• Understand inheritance hierarchy.
• Implement polymorphism using virtual and override.
• Write reusable and extensible code.
Sample Input: 
BaseSalary = 50000
Sample Output: 
Manager Salary = 60000, Developer Salary = 55000*/
using System;
namespace Company
{
    // Base Class
    class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }

        public virtual double CalculateSalary()
        {
            return BaseSalary;
        }
    }
    // Derived Class Manager
    class Manager : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.20);
        }
    }

    // Derived Class Developer
    class Developer : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.10);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Base Salary: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            // Runtime Polymorphism
            Employee emp;

            emp = new Manager();
            emp.BaseSalary = salary;
            Console.WriteLine("Manager Salary = " + emp.CalculateSalary());

            emp = new Developer();
            emp.BaseSalary = salary;
            Console.WriteLine("Developer Salary = " + emp.CalculateSalary());
        }
    }
}