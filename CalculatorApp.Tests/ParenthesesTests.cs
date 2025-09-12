using Xunit;

namespace CalculatorApp.Tests;

public class ParenthesesTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("(2+3)", 5)]
    [InlineData("(3*4)", 12)]
    [InlineData("(8/2)", 4)]
    public void SimpleParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3)*4", 20)]
    [InlineData("2*(3+4)", 14)]
    [InlineData("(5-2)*(3+1)", 12)]
    public void ParenthesesWithOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("((2+3))", 5)]
    [InlineData("(2+(3*4))", 14)]
    [InlineData("((2+3)*4)", 20)]
    public void NestedParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    public void MismatchedParentheses_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Mismatched parentheses.", exception.Message);
    }
}