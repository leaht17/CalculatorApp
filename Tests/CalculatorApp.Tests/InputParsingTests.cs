using Xunit;

namespace CalculatorApp.Tests;

public class InputParsingTests
{
    private readonly Calculator _calculator;

    public InputParsingTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("  2 + 3  ", 5)]
    [InlineData("2+   3", 5)]
    [InlineData("  (2+3)*4  ", 20)]
    public void WhitespaceHandling_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("123", 123)]
    [InlineData("0", 0)]
    [InlineData("999", 999)]
    [InlineData("1000000", 1000000)]
    public void IntegerParsing_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("3.14", 3.14)]
    [InlineData("0.5", 0.5)]
    [InlineData("123.456", 123.456)]
    [InlineData("0.0", 0)]
    [InlineData("1.0", 1)]
    public void DecimalParsing_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData(".5", 0.5)]
    [InlineData(".25", 0.25)]
    [InlineData(".999", 0.999)]
    public void DecimalWithoutLeadingZero_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void EmptyOrWhitespaceInput_ShouldThrowArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("No input provided", exception.Message);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("2+abc")]
    [InlineData("hello")]
    [InlineData("2+3+xyz")]
    public void InvalidCharacters_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2++3")]
    [InlineData("3**4")]
    [InlineData("6//2")]
    [InlineData("2^^3")]
    public void ConsecutiveOperators_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("5--2", 7)] // This actually works: 5 - (-2) = 7
    [InlineData("10--3", 13)] // 10 - (-3) = 13
    [InlineData("2+-3", -1)] // 2 + (-3) = -1
    public void DoubleNegative_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    [InlineData("^")]
    public void OperatorOnly_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2+")]
    [InlineData("5*")]
    [InlineData("3/")]
    [InlineData("4^")]
    public void OperatorAtEnd_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("+2")]
    [InlineData("*3")]
    [InlineData("/4")]
    [InlineData("^5")]
    public void OperatorAtStart_ExceptMinus_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2 3")]
    [InlineData("5 4 + 1")]
    [InlineData("10 20")]
    public void NumbersWithoutOperators_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("-")]
    [InlineData("-(")]
    [InlineData("-+")]
    [InlineData("-*")]
    public void InvalidUnaryMinus_ShouldThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}