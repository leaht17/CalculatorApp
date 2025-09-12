using Xunit;

namespace CalculatorApp.Tests;

public class ExponentiationTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2^3", 8)]
    [InlineData("3^2", 9)]
    [InlineData("5^2", 25)]
    public void BasicExponentiation_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^0", 1)]
    [InlineData("5^0", 1)]
    public void ExponentiationToZero_ShouldReturnOne(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^2^3", 256)] // Right associative: 2^(2^3) = 2^8 = 256
    [InlineData("3^2^2", 81)]  // Right associative: 3^(2^2) = 3^4 = 81
    public void ChainedExponentiation_ShouldBeRightAssociative(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3*4", 32)] // (2^3)*4 = 8*4 = 32
    [InlineData("2*3^2", 18)] // 2*(3^2) = 2*9 = 18
    [InlineData("2+3^2", 11)] // 2+(3^2) = 2+9 = 11
    public void ExponentiationPrecedence_ShouldBeHighest(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }
}