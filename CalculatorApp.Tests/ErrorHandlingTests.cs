using Xunit;

namespace CalculatorApp.Tests;

public class ErrorHandlingTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("5/0")]
    [InlineData("(5+5)/0")]
    public void DivisionByZero_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Division by zero is not allowed.", exception.Message);
    }

    [Theory]
    [InlineData("(")]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    public void MismatchedParentheses_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Mismatched parentheses.", exception.Message);
    }

    [Theory]
    [InlineData("+")]
    [InlineData("2+")]
    [InlineData("*3")]
    public void InsufficientOperands_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyInput_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("No input provided.", exception.Message);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("2+xyz")]
    public void InvalidCharacters_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}