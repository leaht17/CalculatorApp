using System.Text.RegularExpressions;

public class Calculator
{
    public double Evaluate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No input provided.");

        // Split input into numbers, operators, and parentheses
        var tokens = Regex.Matches(input, @"(\d+(\.\d*)?|\.\d+)|[+\-*/^()]");
        if (tokens.Count == 0)
            throw new ArgumentException("Invalid expression.");

        // Shunting Yard Algorithm for order of operations and parentheses
        var output = new Stack<double>();
        var operators = new Stack<string>();
        int i = 0;
        while (i < tokens.Count)
        {
            var token = tokens[i].Value.Trim();
            if (string.IsNullOrEmpty(token))
            {
                i++;
                continue; // skip whitespace
            }
            if (double.TryParse(token, out double num))
            {
                output.Push(num);
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    ApplyOperator(output, operators.Pop());
                }
                if (operators.Count == 0 || operators.Pop() != "(")
                    throw new ArgumentException("Mismatched parentheses.");
            }
            else // operator
            {
                while (operators.Count > 0 && operators.Peek() != "(" &&
                    (IsRightAssociative(token)
                        ? Precedence(operators.Peek()) > Precedence(token)
                        : Precedence(operators.Peek()) >= Precedence(token)))
                {
                    ApplyOperator(output, operators.Pop());
                }
                operators.Push(token);
            }
            i++;
        }
        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op == "(" || op == ")")
                throw new ArgumentException("Mismatched parentheses.");
            ApplyOperator(output, op);
        }
        if (output.Count != 1)
            throw new ArgumentException("Invalid expression.");
        return output.Pop();
    }

    private int Precedence(string op)
    {
        return op switch
        {
            "^" => 3,
            "*" or "/" => 2,
            "+" or "-" => 1,
            _ => 0
        };
    }

    private bool IsRightAssociative(string op)
    {
        return op == "^";
    }

    private void ApplyOperator(Stack<double> output, string op)
    {
        if (output.Count < 2)
            throw new ArgumentException("Insufficient operands for operator.");
        double b = output.Pop();
        double a = output.Pop();
        double result = op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b == 0 ? throw new ArgumentException("Division by zero is not allowed.") : a / b,
            "^" => Math.Pow(a, b),
            _ => throw new ArgumentException($"Unknown operator: {op}")
        };
        output.Push(result);
    }
}
