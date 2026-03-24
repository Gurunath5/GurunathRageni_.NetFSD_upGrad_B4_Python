using System;

class Program
{
    static void Main(string[] args)
    {
        AreaCalculator calculator = new AreaCalculator();

        Shape rectangle = new Rectangle(10, 5);
        Shape circle = new Circle(7);

        // LSP in action
        calculator.PrintArea(rectangle);
        calculator.PrintArea(circle);

        Console.ReadLine();
    }
}