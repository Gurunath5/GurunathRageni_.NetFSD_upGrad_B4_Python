using System;

public class DiscountCalculator
{
    private readonly IDiscountStrategy _discountStrategy;

    public DiscountCalculator(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy
            ?? throw new ArgumentNullException(nameof(discountStrategy));
    }

    public double CalculateFinalPrice(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        double discount = _discountStrategy.CalculateDiscount(amount);
        return amount - discount;
    }
}