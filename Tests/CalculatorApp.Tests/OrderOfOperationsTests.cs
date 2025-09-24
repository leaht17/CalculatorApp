using Xunit;

namespace CalculatorApp.Tests;

public class OrderOfOperationsTests
{
    private readonly Calculator _calculator;

    public OrderOfOperationsTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("2+3*4", 14)]  // 2 + (3*4) = 2 + 12 = 14
    [InlineData("10-2*3", 4)] // 10 - (2*3) = 10 - 6 = 4
    [InlineData("20/4+2", 7)] // (20/4) + 2 = 5 + 2 = 7
    [InlineData("3*4+5", 17)] // (3*4) + 5 = 12 + 5 = 17
    // [InlineData("-2+3", 1)]  // negation has higher precedence than addition: (-2) + 3 = 1
    // [InlineData("-2*3", -6)] // negation has higher precedence than multiplication: (-2) * 3 = -6
    public void MultiplicationDivision_OverAdditionSubtraction_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2^3*4", 32)] // (2^3) * 4 = 8 * 4 = 32
    [InlineData("3*2^2", 12)] // 3 * (2^2) = 3 * 4 = 12
    [InlineData("2^2+3^2", 13)] // (2^2) + (3^2) = 4 + 9 = 13
    [InlineData("10-2^3", 2)] // 10 - (2^3) = 10 - 8 = 2
    // [InlineData("-2^2", -4)] // negation has lower precedence than exponentiation: -(2^2) = -4
    // [InlineData("(-2)^2", 4)] // parentheses override precedence: (-2)^2 = 4
    public void Exponentiation_OverMultiplicationDivision_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2+3*4-5", 9)]   // 2 + (3*4) - 5 = 2 + 12 - 5 = 9
    [InlineData("10/2+3*4", 17)] // (10/2) + (3*4) = 5 + 12 = 17
    [InlineData("2^2*3+1", 13)]  // (2^2) * 3 + 1 = 4 * 3 + 1 = 13
    [InlineData("20-3*2^2", 8)]  // 20 - (3 * (2^2)) = 20 - (3 * 4) = 20 - 12 = 8
    // [InlineData("-2^2+3", -1)] // negation, then exponentiation, then addition: -(2^2) + 3 = -4 + 3 = -1
    public void ComplexExpressions_ShouldFollowCorrectOrder(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("5+3*2-1", 10)] // 5 + (3*2) - 1 = 5 + 6 - 1 = 10
    [InlineData("1-3*2+5", 0)]  // 1 - (3*2) + 5 = 1 - 6 + 5 = 0
    [InlineData("8/2*3+1", 13)] // (8/2) * 3 + 1 = 4 * 3 + 1 = 13
    [InlineData("10*2/5+3", 7)] // (10*2) / 5 + 3 = 20/5 + 3 = 4 + 3 = 7
    public void LeftToRightEvaluation_SamePrecedence_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2*3+4*5", 26)]  // (2*3) + (4*5) = 6 + 20 = 26
    [InlineData("10/2-8/4", 3)]  // (10/2) - (8/4) = 5 - 2 = 3
    [InlineData("3^2+2^3", 17)]  // (3^2) + (2^3) = 9 + 8 = 17
    [InlineData("4*2^2/2", 8)]   // 4 * (2^2) / 2 = 4 * 4 / 2 = 16/2 = 8
    // [InlineData("3*-2", -6)]  // negation in the middle of expression: 3 * (-2) = -6
    // [InlineData("-2*-3", 6)]  // double negation: (-2) * (-3) = 6
    public void MultipleSamePrecedenceOperators_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(2+3)*4", 20)]     // Override precedence: (2+3) * 4 = 5 * 4 = 20
    [InlineData("2*(3+4)", 14)]     // Override precedence: 2 * (3+4) = 2 * 7 = 14
    [InlineData("(10-5)*2", 10)]    // Override precedence: (10-5) * 2 = 5 * 2 = 10
    [InlineData("20/(4+1)", 4)]     // Override precedence: 20 / (4+1) = 20/5 = 4
    // [InlineData("-(2+3)", -5)]   // negation of parenthetical expression: -(2+3) = -5
    public void ParenthesesOverridePrecedence_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("(2+3)^2", 25)]     // (2+3)^2 = 5^2 = 25
    [InlineData("2^(1+2)", 8)]      // 2^(1+2) = 2^3 = 8
    [InlineData("(3*2)^2", 36)]     // (3*2)^2 = 6^2 = 36
    [InlineData("3^(2*2)", 81)]     // 3^(2*2) = 3^4 = 81
    public void ParenthesesWithExponentiation_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2^3^2", 512)]      // Right associative: 2^(3^2) = 2^9 = 512
    [InlineData("(2^3)^2", 64)]     // Left to right with parentheses: (2^3)^2 = 8^2 = 64
    [InlineData("2^(3^2)", 512)]    // Explicit right associativity: 2^(3^2) = 2^9 = 512
    public void ExponentiationAssociativity_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("1+2*3+4*5+6", 33)]           // 1 + (2*3) + (4*5) + 6 = 1 + 6 + 20 + 6 = 33
    [InlineData("2^2+3^2*2-1", 21)]           // (2^2) + ((3^2)*2) - 1 = 4 + (9*2) - 1 = 4 + 18 - 1 = 21
    [InlineData("(1+2)*(3+4)/(2+3)", 4.2)]   // (1+2) * (3+4) / (2+3) = 3 * 7 / 5 = 21/5 = 4.2
    [InlineData("10-2*3+4^2/8", 6)]           // 10 - (2*3) + ((4^2)/8) = 10 - 6 + (16/8) = 10 - 6 + 2 = 6
    public void ComplexMixedOperations_ShouldReturnCorrectResult(string expression, double expected)
    {
        var result = _calculator.Evaluate(expression);
        Assert.Equal(expected, result, 10);
    }
}