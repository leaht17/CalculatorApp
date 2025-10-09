using System.Text.RegularExpressions;

public class Calculator
{
    private int Precedence(string op)
    {
        return op switch
        {
            "^" => 4, // Highest precedence
            "u-" => 3, // Unary minus, lower than exponentiation
            "*" or "/" => 2,
            "+" or "-" => 1,
            _ => 0
        };
    }

    public double Evaluate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No input provided.");

        var tokens = Regex.Matches(input, @"(\d+(\.\d*)?|\.\d+)|[+\-*/^()]\s*");
        if (tokens.Count == 0)
            throw new ArgumentException("Syntax error: invalid expression.");

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
                continue;
            }

            // Handle unary minus as operator
            if (token == "-" && (prevToken == null || prevToken == "(" || IsOperator(prevToken)))
            {
                operators.Push("u-");
                prevToken = "u-";
                i++;
                continue;
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

    private void ApplyOperator(Stack<double> output, string op)
    {
        if (op == "u-")
        {
            if (output.Count < 1)
                throw new ArgumentException("Syntax error: insufficient operands for unary minus.");
            double value = output.Pop();
            output.Push(-value);
            return;
        }
        if (output.Count < 2)
            throw new ArgumentException("Syntax error: insufficient operands for operator.");
        double rightOperand = output.Pop();
        double leftOperand = output.Pop();
        double result;
        if (op == "^")
        {
            double pow = Math.Pow(leftOperand, rightOperand);
            if (double.IsNaN(pow))
                throw new ArgumentException("Invalid operation: negative base with fractional exponent is not allowed.");
            if (double.IsInfinity(pow))
                throw new ArgumentException("Invalid operation: exponentiation result is too large.");
            result = pow;
        }
        else
        {
            result = op switch
            {
                "+" => leftOperand + rightOperand,
                "-" => leftOperand - rightOperand,
                "*" => leftOperand * rightOperand,
                "/" => rightOperand == 0 ? throw new ArgumentException("Invalid operation: division by zero is not allowed.") : leftOperand / rightOperand,
                _ => throw new ArgumentException($"Invalid operation: unknown operator: {op}")
            };
        }
        output.Push(result);
    }

    private bool IsRightAssociative(string op)
    {
        return op == "^";
    }

    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }
}
