using Xunit;

namespace CalculatorApp.Tests;

public class BasicOperationsTests
{
    private readonly Calculator _calculator;

    public BasicOperationsTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("2+3", 5)]
    [InlineData("10+5", 15)]
    [InlineData("0+0", 0)]
    [InlineData("1.5+2.5", 4)]
    [InlineData("100+200", 300)]
    public void Addition_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("5-3", 2)]
    [InlineData("10-15", -5)]
    [InlineData("0-5", -5)]
    [InlineData("7.5-2.5", 5)]
    [InlineData("100-50", 50)]
    public void Subtraction_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2*3", 6)]
    [InlineData("5*4", 20)]
    [InlineData("0*100", 0)]
    [InlineData("2.5*4", 10)]
    [InlineData("7*8", 56)]
    public void Multiplication_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("6/2", 3)]
    [InlineData("15/3", 5)]
    [InlineData("7.5/2.5", 3)]
    [InlineData("100/4", 25)]
    [InlineData("1/2", 0.5)]
    public void Division_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("5/0")]
    [InlineData("10/0")]
    [InlineData("1/0")]
    public void Division_ByZero_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("division by zero", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    // [Theory]
    // [InlineData("-5", -5)]
    // [InlineData("-10", -10)]
    // [InlineData("-3.5", -3.5)]
    // [InlineData("-0", 0)]
    // public void NegativeNumbers_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result, 10);
    // }

    // [Theory]
    // [InlineData("-5+3", -2)]
    // [InlineData("-10+15", 5)]
    // [InlineData("5+-3", 2)]
    // [InlineData("-5*2", -10)]
    // [InlineData("-6/2", -3)]
    // public void NegativeNumbers_WithOperations_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result, 10);
    // }
}