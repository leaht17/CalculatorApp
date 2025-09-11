using Xunit;

namespace CalculatorApp.Tests;

public class InputParsingTests
{
    private readonly Calculator _calculator;

    public InputParsingTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_EmptyString_ThrowsArgumentException()
    {
        // Arrange
        string expression = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_NullInput_ThrowsArgumentException()
    {
        // Arrange
        string? expression = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression!));
    }

    [Fact]
    public void Evaluate_WhitespaceOnly_ThrowsArgumentException()
    {
        // Arrange
        string expression = "   ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_InvalidCharacters_ThrowsArgumentException()
    {
        // Arrange
        string expression = "2 + a";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_ExpressionWithSpaces_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "  2   +   3  ";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Evaluate_ConsecutiveOperators_ThrowsArgumentException()
    {
        // Arrange
        string expression = "2 + + 3";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_OperatorAtEnd_ThrowsArgumentException()
    {
        // Arrange
        string expression = "2 + 3 +";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_OperatorAtStart_ThrowsArgumentException()
    {
        // Arrange
        string expression = "+ 2 + 3";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_SingleNumber_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "42";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Evaluate_DecimalStartingWithDot_ReturnsCorrectResult()
    {
        // Arrange
        string expression = ".5 + 1.5";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(2.0, result);
    }
}