using System.Text.RegularExpressions;

public class Calculator
{
    public double Evaluate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No input provided.");

        // Replace constant names before tokenizing
        input = Regex.Replace(input, @"\bpi\b", Math.PI.ToString("R"), RegexOptions.IgnoreCase);
        input = Regex.Replace(input, @"\be\b", Math.E.ToString("R"), RegexOptions.IgnoreCase);

        // Split input into numbers, operators, and parentheses
        var tokens = Regex.Matches(input, @"(\d+(\.\d*)?|\.\d+)|[+\-*/%^()]");
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
                        // Push a special unary negation operator
                        operators.Push("~");
                        operators.Push("(");
                        prevToken = "(";
                        i++;
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
            "*" or "/" or "%" => 2,
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
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "%" || token == "^";
    }

    private void ApplyOperator(Stack<double> output, string op)
    {
        // Handle unary negation operator
        if (op == "~")
        {
            if (output.Count < 1)
                throw new ArgumentException("Syntax error: insufficient operands for unary operator.");
            double value = output.Pop();
            output.Push(-value);
            return;
        }

        if (output.Count < 2)
            throw new ArgumentException("Syntax error: insufficient operands for operator.");

        double b = output.Pop();
        double a = output.Pop();

        double result = op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" when b == 0 => throw new ArgumentException("Invalid operation: division by zero is not allowed."),
            "/" => a / b,
            "%" when b == 0 => throw new ArgumentException("Invalid operation: modulo by zero is not allowed."),
            "%" => a % b,
            "^" => ValidateAndCalculatePower(a, b),
            _ => throw new ArgumentException($"Invalid operation: unknown operator: {op}")
        };

        output.Push(result);
    }

    private static double ValidateAndCalculatePower(double a, double b)
    {
        if (!double.IsFinite(a))
            throw new ArgumentException("Invalid operation: base must be a finite number.");
        if (!double.IsFinite(b))
            throw new ArgumentException("Invalid operation: exponent must be a finite number.");

        const double epsilon = 1e-9;
        double roundedB = Math.Round(b);
        bool isIntegerExponent = Math.Abs(b - roundedB) < epsilon;

        if (a < 0 && !isIntegerExponent)
            throw new ArgumentException("Invalid operation: negative base with fractional exponent is not allowed.");

        double exponent = isIntegerExponent ? roundedB : b;
        double pow = Math.Pow(a, exponent);
        if (double.IsNaN(pow))
            throw new ArgumentException("Invalid operation: exponentiation result is not a number (NaN).");
        if (double.IsPositiveInfinity(pow) || double.IsNegativeInfinity(pow))
            throw new ArgumentException("Invalid operation: exponentiation result is out of range.");
        return pow;
    }
}
