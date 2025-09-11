using Xunit;

namespace CalculatorApp.Tests;

public class NegationTests
{
    private readonly Calculator _calculator;

    public NegationTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Evaluate_SubtractionAsNegation_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "5 - 3";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Evaluate_NegativeResultFromSubtraction_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "3 - 5";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(-2.0, result);
    }

    [Fact]
    public void Evaluate_ZeroMinusNumber_ReturnsNegativeResult()
    {
        // Arrange
        string expression = "0 - 7";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(-7.0, result);
    }

    [Fact]
    public void Evaluate_ComplexExpressionWithNegativeResult_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "2 * 3 - 10";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(-4.0, result); // 6 - 10 = -4
    }

    [Fact]
    public void Evaluate_SubtractionChain_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "10 - 3 - 2";

        // Act
        double result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(5.0, result); // ((10 - 3) - 2) = 7 - 2 = 5
    }
}