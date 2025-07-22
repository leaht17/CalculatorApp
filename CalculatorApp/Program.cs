using System;

Console.WriteLine("Console Calculator");
Console.WriteLine("Type 'q' to quit. Type '/help' for directions.");

var calculator = new Calculator();
while (true)
{
    Console.Write("Enter an expression: ");
    string input = Console.ReadLine();
    if (input == null)
        break;
    input = input.Trim();
    if (input.ToLower() == "q")
        break;
    if (input.ToLower() == "/help")
    {
        Console.WriteLine("Calculator Help:");
        Console.WriteLine("- Enter mathematical expressions using +, -, *, /, ^");
        Console.WriteLine("- Type 'q' to quit the calculator");
        Console.WriteLine("- Type '/help' to view these directions again");
        Console.WriteLine();
        continue;
    }
    try
    {
        double result = calculator.Evaluate(input);
        Console.WriteLine($"= {result}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine();
    }
}
Console.WriteLine("Calculator session ended.");
