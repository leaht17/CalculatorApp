using Xunit;

namespace CalculatorApp.Tests;

public class ParenthesesTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("(2+3)", 5)]
    [InlineData("(10-5)", 5)]
    [InlineData("(3*4)", 12)]
    [InlineData("(8/2)", 4)]
    public void SimpleParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3)*4", 20)]
    [InlineData("2*(3+4)", 14)]
    [InlineData("(5-2)*(3+1)", 12)]
    [InlineData("(10/2)+(3*2)", 11)]
    [InlineData("(8-3)/(2+3)", 1)]
    public void ParenthesesWithOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("((2+3))", 5)]
    [InlineData("(2+(3*4))", 14)]
    [InlineData("((2+3)*4)", 20)]
    [InlineData("(2*(3+4))", 14)]
    [InlineData("((2+3)*(4+1))", 25)]
    public void NestedParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(((2+3)*4)+1)", 21)]
    [InlineData("(2+((3*4)+1))", 15)]
    [InlineData("((2+3)*(4+(5*2)))", 70)]
    public void DeeplyNestedParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3)*(4+5)", 45)]
    [InlineData("(1+2)+(3+4)", 10)]
    [InlineData("(5-2)*(3-1)", 6)]
    [InlineData("(8/2)+(6/3)", 6)]
    public void MultipleParentheses_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    [InlineData("(2+3))")]
    [InlineData(")(2+3)")]
    public void MismatchedParentheses_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Mismatched parentheses.", exception.Message);
    }

    [Theory]
    [InlineData("()")]
    [InlineData("(+)")]
    [InlineData("(*)")]
    public void EmptyOrInvalidParentheses_ShouldThrowArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }
}