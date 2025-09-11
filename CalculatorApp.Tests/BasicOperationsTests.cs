using Xunit;

namespace CalculatorApp.Tests;

public class BasicOperationsTests
{
    private readonly Calculator _calculator;

    public BasicOperationsTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_Addition_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 + 3";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Evaluate_Subtraction_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "10 - 4";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(6.0, result);
    }

    [Fact]
    public void Evaluate_Multiplication_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "6 * 7";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Evaluate_Division_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "15 / 3";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Evaluate_DivisionByZero_ThrowsArgumentException()
    {
        // Arrange
        string expression = "5 / 0";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_DecimalNumbers_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "3.5 + 2.1";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(5.6, result, 10);
    }

    [Fact]
    public void Evaluate_ComplexExpression_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 + 3 * 4 - 1";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert  
        Assert.Equal(13.0, result); // 2 + 12 - 1 = 13
    }
}