using Xunit;

namespace CalculatorApp.Tests;

public class BasicOperationsTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2+3", 5)]
    [InlineData("0+0", 0)]
    [InlineData("1.5+2.5", 4)]
    [InlineData("1+2", 3.0)]
    public void Addition_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("5-3", 2)]
    [InlineData("10-15", -5)]
    [InlineData("3.5-1.5", 2)]
    public void Subtraction_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2*3", 6)]
    [InlineData("0*100", 0)]
    [InlineData("1.5*2", 3)]
    public void Multiplication_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("6/2", 3)]
    [InlineData("0/5", 0)]
    [InlineData("7.5/2.5", 3)]
    public void Division_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1+2+3", 6)]
    [InlineData("2*3*4", 24)]
    [InlineData("1+2*3", 7)] // Order of operations
    public void ChainedOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }
}