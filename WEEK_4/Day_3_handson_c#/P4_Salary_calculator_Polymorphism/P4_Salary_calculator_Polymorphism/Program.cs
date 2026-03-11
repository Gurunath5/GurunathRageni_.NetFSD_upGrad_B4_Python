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