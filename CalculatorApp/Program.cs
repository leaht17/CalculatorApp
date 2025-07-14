using System;

Console.WriteLine("Console Calculator");
Console.WriteLine("Enter an expression (e.g., 5 + 2 - 3):");
string input = Console.ReadLine();

var calculator = new Calculator();
try
{
    double result = calculator.Evaluate(input);
    Console.WriteLine($"Result: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
