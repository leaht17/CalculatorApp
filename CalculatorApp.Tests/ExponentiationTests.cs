using Xunit;

namespace CalculatorApp.Tests;

public class ExponentiationTests
{
    private readonly Calculator _calculator;

    public ExponentiationTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_SimpleExponentiation_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 ^ 3";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(8.0, result);
    }

    [Fact]
    public void Evaluate_ExponentiationWithDecimals_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "4 ^ 0.5";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Evaluate_ExponentiationRightAssociative_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 ^ 3 ^ 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(512.0, result); // 2^(3^2) = 2^9 = 512
    }

    [Fact]
    public void Evaluate_ExponentiationWithParentheses_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "(2 ^ 3) ^ 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(64.0, result); // (2^3)^2 = 8^2 = 64
    }

    [Fact]
    public void Evaluate_ExponentiationPrecedence_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 + 3 ^ 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(11.0, result); // 2 + (3^2) = 2 + 9 = 11
    }
}