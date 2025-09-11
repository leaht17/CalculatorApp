using Xunit;

namespace CalculatorApp.Tests;

public class OrderOfOperationsTests
{
    private readonly Calculator _calculator;

    public OrderOfOperationsTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_OrderOfOperations_AdditionMultiplication_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 + 3 * 4";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(14.0, result); // 2 + (3 * 4) = 2 + 12 = 14
    }

    [Fact]
    public void Evaluate_OrderOfOperations_MultiplicationDivision_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "12 / 3 * 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(8.0, result); // (12 / 3) * 2 = 4 * 2 = 8
    }

    [Fact]
    public void Evaluate_OrderOfOperations_ExponentiationMultiplication_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 * 3 ^ 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(18.0, result); // 2 * (3 ^ 2) = 2 * 9 = 18
    }
}