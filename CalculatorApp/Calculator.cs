using System.Text.RegularExpressions;

public class Calculator
{
    private const double IntegerComparisonTolerance = 1e-9;

    public double Evaluate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No input provided.");

        // Normalize by removing whitespace so unsupported characters can be detected reliably.
        string normalizedInput = Regex.Replace(input, @"\s+", string.Empty);

        // Split input into numbers, operators, and parentheses
        var tokens = Regex.Matches(normalizedInput, @"(\d+(\.\d*)?|\.\d+)|[+\-*/^()]");
        if (tokens.Count == 0)
            throw new ArgumentException("Syntax error: invalid expression.");

        string matchedTokens = string.Concat(tokens.Select(t => t.Value));
        if (!string.Equals(matchedTokens, normalizedInput, StringComparison.Ordinal))
            throw new ArgumentException("Invalid input: expression contains non-numeric or unsupported tokens.");

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

        double result = op switch
        {
            "+" => AddWithOverflowValidation(a, b),
            "-" => SubtractWithOverflowValidation(a, b),
            "*" => MultiplyWithOverflowValidation(a, b),
            "/" when b == 0 => throw new DivideByZeroException("Invalid operation: division by zero is not allowed."),
            "/" => DivideWithOverflowValidation(a, b),
            "^" => ValidateAndCalculatePower(a, b),
            _ => throw new ArgumentException($"Invalid operation: unknown operator: {op}")
        };
        
        output.Push(result);
    }

    private static double AddWithOverflowValidation(double a, double b)
    {
        return ExecuteWithOverflowValidation(a, b, (left, right) => checked(left + right), (left, right) => left + right, "addition");
    }

    private static double SubtractWithOverflowValidation(double a, double b)
    {
        return ExecuteWithOverflowValidation(a, b, (left, right) => checked(left - right), (left, right) => left - right, "subtraction");
    }

    private static double MultiplyWithOverflowValidation(double a, double b)
    {
        return ExecuteWithOverflowValidation(a, b, (left, right) => checked(left * right), (left, right) => left * right, "multiplication");
    }

    private static double ExecuteWithOverflowValidation(
        double a,
        double b,
        Func<int, int, int> intOperation,
        Func<double, double, double> floatingOperation,
        string operationName,
        bool returnFloatingResultOnIntOperands = false)
    {
        if (TryGetIntOperands(a, b, out int left, out int right))
        {
            try
            {
                if (returnFloatingResultOnIntOperands)
                {
                    intOperation(left, right);
                    return floatingOperation(a, b);
                }

                return intOperation(left, right);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Integer overflow: {operationName} result exceeds Int32 range.", ex);
            }
        }

        return floatingOperation(a, b);
    }

    private static double DivideWithOverflowValidation(double a, double b)
    {
        return ExecuteWithOverflowValidation(
            a,
            b,
            (left, right) => left == int.MinValue && right == -1 ? throw new OverflowException() : 0,
            (left, right) => left / right,
            "division",
            returnFloatingResultOnIntOperands: true);
    }

    private static bool TryGetIntOperands(double a, double b, out int left, out int right)
    {
        left = 0;
        right = 0;

        if (!IsIntegerValue(a) || !IsIntegerValue(b))
            return false;
        if (!IsInInt32Range(a) || !IsInInt32Range(b))
            return false;

        left = (int)a;
        right = (int)b;
        return true;
    }

    private static bool IsIntegerValue(double value)
    {
        return double.IsFinite(value) && Math.Abs(value - Math.Truncate(value)) < IntegerComparisonTolerance;
    }

    private static bool IsInInt32Range(double value)
    {
        return value >= int.MinValue && value <= int.MaxValue;
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
