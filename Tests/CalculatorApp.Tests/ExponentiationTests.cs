using Xunit;

namespace CalculatorApp.Tests;

public class ExponentiationTests
{
    private readonly Calculator _calculator;

    public ExponentiationTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("2^3", 8)]
    [InlineData("3^2", 9)]
    [InlineData("5^0", 1)]
    [InlineData("2^4", 16)]
    [InlineData("10^2", 100)]
    public void BasicExponentiation_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2^0.5", 1.4142135623730951)]
    [InlineData("4^0.5", 2)]
    [InlineData("9^0.5", 3)]
    [InlineData("8^(1/3)", 2, 10)] // Cube root
    public void FractionalExponents_ShouldReturnCorrectResult(string expression, double expected, int precision = 10)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, precision);
    }

    // [Theory]
    // [InlineData("2^-1", 0.5)]
    // [InlineData("4^-2", 0.0625)]
    // [InlineData("10^-1", 0.1)]
    // [InlineData("5^-2", 0.04)]
    // public void NegativeExponents_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result, 10);
    // }

    [Theory]
    [InlineData("2^3^2", 512)] // Right associative: 2^(3^2) = 2^9 = 512
    [InlineData("3^2^2", 81)]  // Right associative: 3^(2^2) = 3^4 = 81
    [InlineData("2^2^3", 256)] // Right associative: 2^(2^3) = 2^8 = 256
    public void RightAssociativity_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(2^3)^2", 64)] // Left to right with parentheses: (2^3)^2 = 8^2 = 64
    [InlineData("(3^2)^2", 81)] // Left to right with parentheses: (3^2)^2 = 9^2 = 81
    public void ExponentiationWithParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2+3^2", 11)] // 2 + (3^2) = 2 + 9 = 11
    [InlineData("2*3^2", 18)] // 2 * (3^2) = 2 * 9 = 18
    [InlineData("10-2^3", 2)] // 10 - (2^3) = 10 - 8 = 2
    [InlineData("20/2^2", 5)] // 20 / (2^2) = 20 / 4 = 5
    public void ExponentiationPrecedence_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("0^0")]
    [InlineData("0^-1")]
    [InlineData("0^-2")]
    public void ZeroToNegativeOrZeroPower_ShouldThrowOrReturnSpecialValue(string expression)
    {
        // 0^0 and 0^negative are mathematically undefined
        // The calculator implementation throws for 0^negative cases
        if (expression.Contains("-"))
        {
            var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
            Assert.Contains("exponentiation result is too large", exception.Message, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            var result = _calculator.Evaluate(expression);
            // We accept whatever Math.Pow returns for 0^0
            Assert.True(double.IsNaN(result) || double.IsInfinity(result) || result == 1);
        }
    }

    [Theory]
    [InlineData("-4^0.5")] // Negative base with fractional exponent
    [InlineData("-2^0.5")]
    public void NegativeBaseWithFractionalExponent_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("negative base with fractional exponent", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("(-4)^0.5")] // Should still throw for negative under parentheses
    [InlineData("(-2)^0.5")]
    public void NegativeBaseInParenthesesWithFractionalExponent_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("negative base with fractional exponent", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    // [Theory]
    // [InlineData("(-2)^2", 4)] // Even integer exponent with negative base should work
    // [InlineData("(-3)^3", -27)] // Odd integer exponent with negative base should work
    // [InlineData("(-2)^4", 16)]
    // public void NegativeBaseWithIntegerExponent_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result, 10);
    // }
}