using Xunit;

namespace CalculatorApp.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    #region Basic Operations Tests

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

    #endregion

    #region Parentheses Tests

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

    #endregion

    #region Exponentiation Tests

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

    #endregion

    #region Negation Tests

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

    #endregion

    #region Input Parsing Tests

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

    #endregion

    #region Order of Operations Tests

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

    #endregion
}