using System;

class Program
{
    static void Main(string[] args)
    {
        double amount = 1000;

        // Choose strategy (can be dynamic)
        IDiscountStrategy discountStrategy = new VipCustomerDiscount();

        DiscountCalculator calculator = new DiscountCalculator(discountStrategy);

        double finalPrice = calculator.CalculateFinalPrice(amount);

        Console.WriteLine("Original Amount: " + amount);
        Console.WriteLine("Final Price after Discount: " + finalPrice);

        Console.ReadLine();
    }
}
