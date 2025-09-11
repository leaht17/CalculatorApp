using Xunit;

namespace CalculatorApp.Tests;

public class BasicOperationsTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2+3", 5)]
    [InlineData("10+15", 25)]
    [InlineData("0+0", 0)]
    [InlineData("1.5+2.5", 4)]
    [InlineData("100+200", 300)]
    public void Addition_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("5-3", 2)]
    [InlineData("10-15", -5)]
    [InlineData("0-0", 0)]
    [InlineData("3.5-1.5", 2)]
    [InlineData("100-50", 50)]
    public void Subtraction_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2*3", 6)]
    [InlineData("5*4", 20)]
    [InlineData("0*100", 0)]
    [InlineData("1.5*2", 3)]
    [InlineData("7*8", 56)]
    public void Multiplication_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("6/2", 3)]
    [InlineData("15/3", 5)]
    [InlineData("0/5", 0)]
    [InlineData("7.5/2.5", 3)]
    [InlineData("100/4", 25)]
    public void Division_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Division_ByZero_ShouldThrowArgumentException()
    {
        // Arrange
        var expression = "5/0";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Equal("Division by zero is not allowed.", exception.Message);
    }

    [Theory]
    [InlineData("1+2+3", 6)]
    [InlineData("10-3-2", 5)]
    [InlineData("2*3*4", 24)]
    [InlineData("24/4/2", 3)]
    [InlineData("1+2*3", 7)] // Tests order of operations
    [InlineData("2*3+4", 10)] // Tests order of operations
    public void ChainedOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }
}