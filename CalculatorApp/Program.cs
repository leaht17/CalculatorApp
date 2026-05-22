using System;

Console.WriteLine("Console Calculator");
Console.WriteLine("Type 'q' or 'quit' to quit. Type 'h' or 'help' for help.");

var calculator = new Calculator();
while (true)
{
    Console.Write("Enter an expression: ");
    string input = Console.ReadLine();
    if (input == null)
        break;
    input = input.Trim();
    if (input.Equals("q", StringComparison.OrdinalIgnoreCase) || input.Equals("quit", StringComparison.OrdinalIgnoreCase))
        break;
    if (input.Equals("h", StringComparison.OrdinalIgnoreCase) || input.Equals("help", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Calculator Help:");
        Console.WriteLine("- Enter mathematical expressions using +, -, *, /, ^");
        Console.WriteLine("- Type 'q' or 'quit' to quit the calculator");
        Console.WriteLine("- Type 'h' or 'help' to view these directions again");
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
