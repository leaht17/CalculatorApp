using Xunit;

namespace CalculatorApp.Tests;

public class OrderOfOperationsTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData("2+3*4", 14)] // Should be 2+(3*4) = 2+12 = 14
    [InlineData("2*3+4", 10)] // Should be (2*3)+4 = 6+4 = 10
    [InlineData("10-2*3", 4)] // Should be 10-(2*3) = 10-6 = 4
    [InlineData("2*3-4", 2)] // Should be (2*3)-4 = 6-4 = 2
    public void MultiplicationBeforeAdditionSubtraction_ShouldFollowPEMDAS(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("8/2+3", 7)] // Should be (8/2)+3 = 4+3 = 7
    [InlineData("2+8/2", 6)] // Should be 2+(8/2) = 2+4 = 6
    [InlineData("12-8/2", 8)] // Should be 12-(8/2) = 12-4 = 8
    [InlineData("8/2-1", 3)] // Should be (8/2)-1 = 4-1 = 3
    public void DivisionBeforeAdditionSubtraction_ShouldFollowPEMDAS(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3*4", 32)] // Should be (2^3)*4 = 8*4 = 32
    [InlineData("2*3^2", 18)] // Should be 2*(3^2) = 2*9 = 18
    [InlineData("2^3/2", 4)] // Should be (2^3)/2 = 8/2 = 4
    [InlineData("8/2^2", 2)] // Should be 8/(2^2) = 8/4 = 2
    public void ExponentiationBeforeMultiplicationDivision_ShouldFollowPEMDAS(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2+3^2*4", 38)] // Should be 2+((3^2)*4) = 2+(9*4) = 2+36 = 38
    [InlineData("2*3^2+4", 22)] // Should be (2*(3^2))+4 = (2*9)+4 = 18+4 = 22
    [InlineData("10-2^2*2", 2)] // Should be 10-((2^2)*2) = 10-(4*2) = 10-8 = 2
    public void ExponentiationMultiplicationAddition_ShouldFollowPEMDAS(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2+3-4", 1)] // Left to right: (2+3)-4 = 5-4 = 1
    [InlineData("10-3+2", 9)] // Left to right: (10-3)+2 = 7+2 = 9
    [InlineData("1+2+3+4", 10)] // Left to right: ((1+2)+3)+4 = (3+3)+4 = 6+4 = 10
    [InlineData("10-2-3", 5)] // Left to right: (10-2)-3 = 8-3 = 5
    public void SamePrecedenceAdditionSubtraction_ShouldBeLeftToRight(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2*3*4", 24)] // Left to right: (2*3)*4 = 6*4 = 24
    [InlineData("2*6/3", 4)] // Left to right: (2*6)/3 = 12/3 = 4
    [InlineData("12/3*2", 8)] // Left to right: (12/3)*2 = 4*2 = 8
    [InlineData("8/2/2", 2)] // Left to right: (8/2)/2 = 4/2 = 2
    public void SamePrecedenceMultiplicationDivision_ShouldBeLeftToRight(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2^3^2", 512)] // Right to left: 2^(3^2) = 2^9 = 512
    [InlineData("2^2^3", 256)] // Right to left: 2^(2^3) = 2^8 = 256
    [InlineData("3^2^2", 81)] // Right to left: 3^(2^2) = 3^4 = 81
    public void ExponentiationAssociativity_ShouldBeRightToLeft(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2+3*4^2", 50)] // Should be 2+(3*(4^2)) = 2+(3*16) = 2+48 = 50
    [InlineData("2^3+4*5", 28)] // Should be (2^3)+(4*5) = 8+20 = 28
    [InlineData("10/2+3^2", 14)] // Should be (10/2)+(3^2) = 5+9 = 14
    [InlineData("2*3^2/3+1", 7)] // Should be ((2*(3^2))/3)+1 = ((2*9)/3)+1 = (18/3)+1 = 6+1 = 7
    public void ComplexMixedOperations_ShouldFollowFullPEMDAS(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(2+3)*4", 20)] // Parentheses override precedence
    [InlineData("2*(3+4)", 14)] // Parentheses override precedence
    [InlineData("(2+3)^2", 25)] // Parentheses override precedence
    [InlineData("2^(3+1)", 16)] // Parentheses override precedence
    [InlineData("(10-2)/2", 4)] // Parentheses override precedence
    public void ParenthesesOverridePrecedence_ShouldCalculateParenthesesFirst(string expression, double expected)
    {
        // Act
        var result = _calculator.Evaluate(expression);

        // Assert
        Assert.Equal(expected, result);
    }
}