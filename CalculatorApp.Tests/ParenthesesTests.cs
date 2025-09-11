using Xunit;

namespace CalculatorApp.Tests;

public class ParenthesesTests
{
    private readonly Calculator _calculator;

    public ParenthesesTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_SimpleParentheses_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "(2 + 3) * 4";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Evaluate_NestedParentheses_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "((2 + 3) * 4) - 5";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(15.0, result);
    }

    [Fact]
    public void Evaluate_MultipleParentheses_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "(2 + 3) * (4 - 1)";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(15.0, result);
    }

    [Fact]
    public void Evaluate_UnmatchedOpenParenthesis_ThrowsArgumentException()
    {
        // Arrange
        string expression = "(2 + 3";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Fact]
    public void Evaluate_UnmatchedCloseParenthesis_ThrowsArgumentException()
    {
        // Arrange
        string expression = "2 + 3)";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}