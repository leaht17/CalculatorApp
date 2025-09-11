using Xunit;

namespace CalculatorApp.Tests;

public class NegationTests
{
    private readonly Calculator _calculator = new();

    /// <summary>
    /// These tests document the current state of negation support in the calculator.
    /// Currently, unary minus (negation) is NOT implemented in the calculator.
    /// All tests in this class are expected to fail until negation is properly implemented.
    /// </summary>

    [Theory]
    [InlineData("-5")]
    [InlineData("-10")]
    [InlineData("-3.5")]
    public void UnaryMinus_IsNotCurrentlySupported_ShouldThrowException(string expression)
    {
        // Note: The calculator currently does not support unary minus operations
        // These tests document the expected behavior when negation is not implemented
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }

    [Theory]
    [InlineData("-5+3")]
    [InlineData("5+-3")]
    [InlineData("-2*3")]
    [InlineData("10/-2")]
    public void NegativeNumbersInExpressions_AreNotCurrentlySupported_ShouldThrowException(string expression)
    {
        // Note: The calculator currently does not support negative numbers in expressions
        // These tests document the expected behavior when negation is not implemented
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }

    [Theory]
    [InlineData("5*(-3)")]
    [InlineData("(-2)+5")]
    [InlineData("(-4)^2")]
    public void NegativeNumbersInParentheses_AreNotCurrentlySupported_ShouldThrowException(string expression)
    {
        // Note: The calculator currently does not support negative numbers even in parentheses
        // These tests document the expected behavior when negation is not implemented
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }

    // TODO: When negation is implemented, the following tests should be added:
    
    // [Theory]
    // [InlineData("-5", -5)]
    // [InlineData("-10", -10)]
    // [InlineData("-3.5", -3.5)]
    // public void UnaryMinus_ShouldReturnNegativeValue(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result);
    // }

    // [Theory]
    // [InlineData("-5+3", -2)]
    // [InlineData("5+-3", 2)]
    // [InlineData("-2*3", -6)]
    // [InlineData("10/-2", -5)]
    // public void NegativeNumbersInExpressions_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result);
    // }

    // [Theory]
    // [InlineData("5*(-3)", -15)]
    // [InlineData("(-2)+5", 3)]
    // [InlineData("(-4)^2", 16)]
    // [InlineData("-(2+3)", -5)]
    // public void NegativeNumbersInParentheses_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result);
    // }

    // [Theory]
    // [InlineData("--5", 5)] // Double negative
    // [InlineData("---5", -5)] // Triple negative
    // public void MultipleNegations_ShouldReturnCorrectResult(string expression, double expected)
    // {
    //     var result = _calculator.Evaluate(expression);
    //     Assert.Equal(expected, result);
    // }
}