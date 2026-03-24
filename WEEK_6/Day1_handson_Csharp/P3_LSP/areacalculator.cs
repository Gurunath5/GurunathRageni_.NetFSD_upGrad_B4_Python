using System;

public class AreaCalculator
{
    public void PrintArea(Shape shape)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));

        Console.WriteLine("Area: " + shape.CalculateArea());
    }
}