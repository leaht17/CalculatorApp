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
            throw new ArgumentException("Syntax error: invalid expression.");

        // Shunting Yard Algorithm for order of operations and parentheses
        var output = new Stack<double>();
        var operators = new Stack<string>();
        int i = 0;
        string prevToken = null;
        while (i < tokens.Count)
        {
            var token = tokens[i].Value.Trim();

            if (string.IsNullOrEmpty(token))
            {
                i++;
                continue; // skip whitespace
            }

            // Handle unary minus (negative numbers)
            if (token == "-" && (prevToken == null || prevToken == "(" || IsOperator(prevToken)))
            {
                // Look ahead for the number
                i++;
                if (i < tokens.Count)
                {
                    var nextToken = tokens[i].Value.Trim();
                    if (double.TryParse(nextToken, out double negNum))
                    {
                        output.Push(-negNum);
                        prevToken = nextToken;
                        i++;
                        continue;
                    }
                    else if (nextToken == "(")
                    {
                        // Support for negative parenthesis: e.g., -(3+2)
                        operators.Push("-");
                        operators.Push("(");
                        prevToken = "-";
                        continue;
                    }
                    else
                    {
                        throw new ArgumentException("Syntax error: invalid use of unary minus.");
                    }
                }
                else
                {
                    throw new ArgumentException("Syntax error: invalid use of unary minus at end of expression.");
                }
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
                    throw new ArgumentException("Syntax error: mismatched parentheses.");
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
            prevToken = token;
            i++;
        }

        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op == "(" || op == ")")
                throw new ArgumentException("Syntax error: mismatched parentheses.");
            ApplyOperator(output, op);
        }

        if (output.Count != 1)
            throw new ArgumentException("Syntax error: invalid expression.");

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

    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }

    private void ApplyOperator(Stack<double> output, string op)
    {
        if (output.Count < 2)
            throw new ArgumentException("Syntax error: insufficient operands for operator.");

        double b = output.Pop();
        double a = output.Pop();
        double result;

        // Catch invalid cases for exponentiation
        if (op == "^")
        {
            double pow = Math.Pow(a, b);
            if (double.IsNaN(pow))
                throw new ArgumentException("Invalid operation: negative base with fractional exponent is not allowed.");
            if (double.IsInfinity(pow))
                throw new ArgumentException("Invalid operation: exponentiation result is too large.");
            result = pow;
        }
        else
        {
            // Handle other operators
            result = op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b == 0 ? throw new ArgumentException("Invalid operation: division by zero is not allowed.") : a / b,
                _ => throw new ArgumentException($"Invalid operation: unknown operator: {op}")
            };
        }
        output.Push(result);
    }
}
