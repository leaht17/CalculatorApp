using Xunit;

namespace CalculatorApp.Tests;

public class InputParsingTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2 + 3", 5)]
    [InlineData(" 2+3 ", 5)]
    [InlineData("2  +  3", 5)]
    [InlineData("  2*3  ", 6)]
    [InlineData("2   ^   3", 8)]
    public void WhitespaceHandling_ShouldIgnoreSpaces(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2.5+1.5", 4)]
    [InlineData("3.14*2", 6.28)]
    [InlineData("10.0/2.5", 4)]
    [InlineData("0.5^2", 0.25)]
    public void DecimalNumbers_ShouldBeParseCorrectly(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result, precision: 10);
    }

    [Theory]
    [InlineData(".5+.5", 1)]
    [InlineData(".25*4", 1)]
    [InlineData("5+.5", 5.5)]
    public void DecimalNumbersStartingWithDot_ShouldBeParseCorrectly(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result, precision: 10);
    }

    [Theory]
    [InlineData("0+0", 0)]
    [InlineData("0*100", 0)]
    [InlineData("0/5", 0)]
    [InlineData("5-5", 0)]
    public void ZeroValues_ShouldBeHandledCorrectly(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123+456", 579)]
    [InlineData("1000*2000", 2000000)]
    [InlineData("999999+1", 1000000)]
    public void LargeNumbers_ShouldBeHandledCorrectly(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void EmptyOrNullInput_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("No input provided.", exception.Message);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("2+abc")]
    [InlineData("2@3")]
    [InlineData("2#3")]
    [InlineData("hello world")]
    public void InvalidCharacters_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    [InlineData("^")]
    [InlineData("++")]
    [InlineData("2+")]
    [InlineData("*3")]
    [InlineData("+5")]
    public void OperatorOnlyOrInvalidOperatorSequence_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2 3")]
    [InlineData("2 3 4")]
    [InlineData("5 10")]
    public void NumbersWithoutOperators_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2+")]
    [InlineData("2*")]
    [InlineData("2^")]
    [InlineData("2/")]
    [InlineData("2-")]
    public void IncompleteExpressions_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}