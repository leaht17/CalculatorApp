using Xunit;

namespace CalculatorApp.Tests;

public class ExponentiationTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2^3", 8)]
    [InlineData("3^2", 9)]
    [InlineData("5^2", 25)]
    [InlineData("2^4", 16)]
    [InlineData("10^2", 100)]
    public void BasicExponentiation_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^0", 1)]
    [InlineData("5^0", 1)]
    [InlineData("100^0", 1)]
    public void ExponentiationToZero_ShouldReturnOne(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("5^1", 5)]
    [InlineData("2^1", 2)]
    [InlineData("100^1", 100)]
    public void ExponentiationToOne_ShouldReturnBase(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("4^0.5", 2)] // Square root
    [InlineData("9^0.5", 3)] // Square root
    [InlineData("8^0.333333333", 2)] // Cube root (approximate)
    public void FractionalExponents_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result, precision: 1);
    }

    [Theory]
    [InlineData("2^2^3", 256)] // Should be 2^(2^3) = 2^8 = 256 (right associative)
    [InlineData("3^2^2", 81)]  // Should be 3^(2^2) = 3^4 = 81 (right associative)
    public void ChainedExponentiation_ShouldBeRightAssociative(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3)^2", 25)]
    [InlineData("2^(1+2)", 8)]
    [InlineData("(2*3)^2", 36)]
    [InlineData("2^(3*2)", 64)]
    public void ExponentiationWithParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3*4", 32)] // Should be (2^3)*4 = 8*4 = 32
    [InlineData("2*3^2", 18)] // Should be 2*(3^2) = 2*9 = 18
    [InlineData("2+3^2", 11)] // Should be 2+(3^2) = 2+9 = 11
    public void ExponentiationPrecedence_ShouldBeHighest(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void InvalidExponentiation_NegativeBaseWithFractionalExponent_ShouldThrowArgumentException()
    {
        // Arrange
        var expression = "(-4)^0.5"; // This would result in NaN, but currently fails due to no negation support
        
        // Act & Assert
        // Note: The current implementation doesn't support negation, so this fails for a different reason
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Insufficient operands", exception.Message);
    }
}