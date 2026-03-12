/*Level-2 Problem 5: Vehicle Rental System
Scenario:
A vehicle rental company wants a system where different vehicle types calculate rental charges differently.
Requirements:
1. Create a base class Vehicle with properties Brand and RentalRatePerDay.
2. Create derived classes Car and Bike.
3. Override CalculateRental(int days) method.
4. Car adds insurance charge of 500 per rental.
5. Bike offers 5% discount on total rental.
Technical Constraints:
• Use encapsulation with proper access modifiers.
• Apply runtime polymorphism.
• Validate number of rental days.
Expectations:
• Use base class reference to call overridden methods.
• Implement clean class hierarchy.
• Display final rental cost.
Learning Outcome:
• Master inheritance and polymorphism.
• Implement real-world OOP scenarios.
• Improve object-oriented design skills.
Sample Input: 
Car RentalRatePerDay = 2000, Days = 3
Sample Output: 
Total Rental = 6500

*/

using System;

namespace VehicleRental
{
    // Base Class
    class Vehicle
    {
        private string brand;
        private double rentalRatePerDay;

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public double RentalRatePerDay
        {
            get { return rentalRatePerDay; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Invalid rental rate");
                }
                else
                {
                    rentalRatePerDay = value;
                }
            }
        }

        public virtual double CalculateRental(int days)
        {
            return rentalRatePerDay * days;
        }
    }

    // Derived Class Car
    class Car : Vehicle
    {
        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("Invalid rental days");
                return 0;
            }

            return (RentalRatePerDay * days) + 500; // insurance charge
        }
    }

    // Derived Class Bike
    class Bike : Vehicle
    {
        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("Invalid rental days");
                return 0;
            }

            double total = RentalRatePerDay * days;
            return total - (total * 0.05); // 5% discount
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Car Rental Rate Per Day: ");
            double rate = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Number of Days: ");
            int days = Convert.ToInt32(Console.ReadLine());

            // Runtime Polymorphism
            Vehicle vehicle = new Car();
            vehicle.RentalRatePerDay = rate;

            Console.WriteLine("Total Rental = " + vehicle.CalculateRental(days));
        }
    }
}