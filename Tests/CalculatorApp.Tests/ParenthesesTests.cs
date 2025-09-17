using Xunit;

namespace CalculatorApp.Tests;

public class ParenthesesTests
{
    private readonly Calculator _calculator;

    public ParenthesesTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("(2+3)", 5)]
    [InlineData("(10-5)", 5)]
    [InlineData("(4*3)", 12)]
    [InlineData("(8/2)", 4)]
    public void SimpleParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(2+3)*4", 20)]
    [InlineData("2*(3+4)", 14)]
    [InlineData("(5-2)*(3+1)", 12)]
    [InlineData("(10/2)+(3*2)", 11)]
    public void ParenthesesWithOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("((2+3)*4)", 20)]
    [InlineData("(2*(3+4))", 14)]
    [InlineData("((5+1)*(3-1))", 12)]
    [InlineData("(((2+1)*3)+1)", 10)]
    public void NestedParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("0-(2+3)", -5)]
    [InlineData("0-(5-2)", -3)]
    [InlineData("10-(4*2)", 2)]
    [InlineData("5-(10/2)", 0)]
    public void SubtractionWithParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(-2)", -2)]
    [InlineData("(-5)", -5)]
    [InlineData("(-3.5)", -3.5)]
    public void NegativeNumbersInParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    [InlineData("(2+3))")]
    [InlineData(")(2+3(")]
    public void MismatchedParentheses_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("parentheses", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("()")]
    [InlineData("2+()")]
    [InlineData("()+3")]
    public void EmptyParentheses_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}