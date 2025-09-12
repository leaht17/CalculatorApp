using Xunit;

namespace CalculatorApp.Tests;

public class OrderOfOperationsTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2+3*4", 14)] // 2+(3*4) = 14
    [InlineData("2*3+4", 10)] // (2*3)+4 = 10
    [InlineData("10-6/2", 7)] // 10-(6/2) = 7
    public void BasicPEMDAS_ShouldFollowCorrectPrecedence(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3*4", 32)] // (2^3)*4 = 32
    [InlineData("2*3^2", 18)] // 2*(3^2) = 18
    [InlineData("2+3^2", 11)] // 2+(3^2) = 11
    public void ExponentiationHasHighestPrecedence(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3^2", 512)] // Right associative: 2^(3^2) = 2^9 = 512
    [InlineData("3^2^2", 81)]  // Right associative: 3^(2^2) = 3^4 = 81
    public void ExponentiationIsRightAssociative(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result);
    }
}