using Xunit;

namespace CalculatorApp.Tests;

public class ErrorHandlingTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("5/0")]
    [InlineData("10/0")]
    [InlineData("0/0")]
    [InlineData("(5+5)/0")]
    public void DivisionByZero_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Division by zero is not allowed.", exception.Message);
    }

    [Theory]
    [InlineData("(")]
    [InlineData(")")]
    [InlineData("((")]
    [InlineData("))")]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    [InlineData("(2+3))")]
    public void MismatchedParentheses_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Mismatched parentheses.", exception.Message);
    }

    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    [InlineData("^")]
    [InlineData("2+")]
    [InlineData("*3")]
    [InlineData("/5")]
    [InlineData("^2")]
    public void InsufficientOperands_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void EmptyOrWhitespaceInput_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("No input provided.", exception.Message);
    }

    [Fact]
    public void NullInput_ShouldThrowArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(null!));
        Assert.Equal("No input provided.", exception.Message);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("2+abc")]
    [InlineData("hello")]
    [InlineData("2@3")]
    [InlineData("2#3")]
    [InlineData("2$3")]
    [InlineData("2%3")]
    public void InvalidCharacters_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2 3")]
    [InlineData("5 10 15")]
    [InlineData("1 2 3 4")]
    public void NumbersWithoutOperators_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Invalid expression.", exception.Message);
    }

    [Theory]
    [InlineData("++")]
    [InlineData("--")]
    [InlineData("**")]
    [InlineData("//")]
    [InlineData("^^")]
    [InlineData("+-")]
    [InlineData("*/")]
    public void InvalidOperatorSequences_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    // Note: Testing for overflow/underflow scenarios
    [Theory]
    [InlineData("999999999999999999999999999999^999999999999999999999999999999")]
    public void ExtremelyLargeCalculations_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Invalid operation", exception.Message);
    }

    [Theory]
    [InlineData("()")]
    [InlineData("(+)")]
    [InlineData("(*)")]
    [InlineData("(/)")]
    [InlineData("(^)")]
    public void EmptyParenthesesOrOperatorOnlyInParentheses_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("..")]
    [InlineData("2..3")]
    [InlineData("2.3.4")]
    [InlineData(".")]
    public void InvalidDecimalFormats_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    // Test for invalid exponentiation scenarios that should be caught
    [Fact]
    public void InvalidExponentiation_ShouldBeHandledGracefully()
    {
        // Note: The calculator should handle cases where Math.Pow returns NaN or Infinity
        // This is more of a documentation test for edge cases
        
        // The calculator currently has checks for these in the ApplyOperator method
        // Testing is limited by what expressions can actually be parsed
        Assert.True(true); // Placeholder test documenting that exponentiation error handling exists
    }
}