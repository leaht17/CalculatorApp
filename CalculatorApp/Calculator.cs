using System.Text.RegularExpressions;

public class Calculator
{
    public double Evaluate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No input provided.");

        // Split input into numbers and operators
        var tokens = Regex.Matches(input, "(\\d+\\.?\\d*)|[+\\-*/^]");
        if (tokens.Count == 0)
            throw new ArgumentException("Invalid expression.");

        // Evaluate left-to-right
        double result = double.Parse(tokens[0].Value);
        for (int i = 1; i < tokens.Count; i += 2)
        {
            string op = tokens[i].Value;
            double next = double.Parse(tokens[i + 1].Value);
            switch (op)
            {
                case "+": result += next; break;
                case "-": result -= next; break;
                case "*": result *= next; break;
                case "/": result /= next; break;
                case "^": result = Math.Pow(result, next); break;
                default:
                    throw new ArgumentException($"Unknown operator: {op}");
            }
        }
        return result;
    }
}
