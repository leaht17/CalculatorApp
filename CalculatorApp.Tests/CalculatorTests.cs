public class CalculatorTests
{
    private readonly Calculator _calc = new();

    // Addition
    [Theory]
    [InlineData("1+2", 3)]
    [InlineData("0+0", 0)]
    [InlineData("1.5+2.5", 4)]
    public void Addition(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Subtraction
    [Theory]
    [InlineData("5-3", 2)]
    [InlineData("0-5", -5)]
    public void Subtraction(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Multiplication
    [Theory]
    [InlineData("3*4", 12)]
    [InlineData("0*99", 0)]
    public void Multiplication(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Division
    [Theory]
    [InlineData("10/2", 5)]
    [InlineData("7/2", 3.5)]
    public void Division(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    [Fact]
    public void Division_ByZero_Throws() =>
        Assert.Throws<ArgumentException>(() => _calc.Evaluate("5/0"));

    // Modulo
    [Theory]
    [InlineData("10%3", 1)]
    [InlineData("9%3", 0)]
    [InlineData("7%4", 3)]
    public void Modulo(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    [Fact]
    public void Modulo_ByZero_Throws() =>
        Assert.Throws<ArgumentException>(() => _calc.Evaluate("5%0"));

    // Modulo edge cases: negative operands
    [Theory]
    [InlineData("-5%2", -1)]  // Result takes sign of dividend
    [InlineData("5%-2", 1)]   // Result takes sign of dividend
    [InlineData("-5%-2", -1)] // Both negative
    public void Modulo_NegativeOperands(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Modulo edge cases: fractional operands
    [Theory]
    [InlineData("5.5%2", 1.5)]
    [InlineData("7.8%2.5", 0.3)]
    [InlineData("10.9%3.2", 1.3)]
    public void Modulo_FractionalOperands(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input), precision: 5);

    // Modulo precedence (same as * and /)
    [Theory]
    [InlineData("8%3*2", 4)]    // (8%3)*2 = 2*2 = 4
    [InlineData("8*3%5", 4)]    // (8*3)%5 = 24%5 = 4
    [InlineData("10%3+2", 3)]   // (10%3)+2 = 1+2 = 3
    [InlineData("10+3%2", 11)]  // 10+(3%2) = 10+1 = 11
    public void Modulo_Precedence(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Modulo with parentheses
    [Theory]
    [InlineData("(8%3)*2", 4)]   // 2*2 = 4
    [InlineData("8%(3*2)", 2)]   // 8%6 = 2
    [InlineData("(10+2)%5", 2)]  // 12%5 = 2
    public void Modulo_WithParentheses(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Exponentiation
    [Theory]
    [InlineData("2^10", 1024)]
    [InlineData("9^0.5", 3)]
    public void Exponentiation(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Unary minus
    [Theory]
    [InlineData("-3+5", 2)]
    [InlineData("-(3+2)", -5)]
    [InlineData("-(3+2)*2", -10)]
    [InlineData("5+-(3+2)", 0)]
    [InlineData("-(-3)", 3)]
    public void UnaryMinus(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Parentheses / order of operations
    [Theory]
    [InlineData("(1+2)*3", 9)]
    [InlineData("2+3*4", 14)]
    [InlineData("10%3+1", 2)]
    public void OrderOfOperations(string input, double expected) =>
        Assert.Equal(expected, _calc.Evaluate(input));

    // Error cases
    [Fact]
    public void EmptyInput_Throws() =>
        Assert.Throws<ArgumentException>(() => _calc.Evaluate(""));

    [Fact]
    public void MismatchedParentheses_Throws() =>
        Assert.Throws<ArgumentException>(() => _calc.Evaluate("(1+2"));
}
