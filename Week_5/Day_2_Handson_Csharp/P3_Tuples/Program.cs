using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Input
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Monthly Sales Amount: ");
            if (!double.TryParse(Console.ReadLine(), out double sales) || sales < 0)
            {
                Console.WriteLine("Invalid sales amount.");
                return;
            }

            Console.Write("Enter Customer Rating (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Invalid rating. Must be between 1 and 5.");
                return;
            }

            // Get Tuple
            var performanceData = GetPerformanceData(sales, rating);

            // Pattern Matching
            string category = performanceData switch
            {
                ( >= 100000, >= 4) => "High Performer",
                ( >= 50000, >= 3) => "Average Performer",
                _ => "Needs Improvement"
            };

            // Output
            Console.WriteLine("\n===== Employee Performance =====");
            Console.WriteLine("Employee Name: " + name);
            Console.WriteLine("Sales Amount: " + performanceData.sales);
            Console.WriteLine("Rating: " + performanceData.rating);
            Console.WriteLine("Performance: " + category);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }

    // Method returning Tuple
    static (double sales, int rating) GetPerformanceData(double sales, int rating)
    {
        return (sales, rating);
    }
}