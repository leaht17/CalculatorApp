# Calculator Test Suite

This directory contains comprehensive tests for the Calculator application, organized into separate files by functionality.

## Test Structure

### 1. BasicOperationsTests.cs (93 lines)
Tests for fundamental arithmetic operations:
- Addition, subtraction, multiplication, division
- Negative numbers and unary minus
- Division by zero error handling
- Edge cases with decimal numbers

### 2. ParenthesesTests.cs (87 lines) 
Tests for parentheses handling:
- Simple parentheses expressions
- Nested parentheses
- Parentheses with arithmetic operations
- Negative numbers in parentheses
- Mismatched parentheses error handling
- Empty parentheses error handling

### 3. ExponentiationTests.cs (125 lines)
Tests for exponentiation (^) operations:
- Basic exponentiation
- Fractional and negative exponents
- Right associativity of exponentiation (2^3^2 = 2^(3^2))
- Precedence over other operations
- Error handling for invalid operations (negative base with fractional exponent)
- Special cases like 0^0 and 0^negative

### 4. InputParsingTests.cs (147 lines)
Tests for input validation and parsing:
- Whitespace handling
- Integer and decimal number parsing
- Numbers without leading zeros (.5)
- Invalid characters and malformed expressions
- Consecutive operators
- Empty input handling
- Unary minus validation

### 5. OrderOfOperationsTests.cs (110 lines)
Tests for mathematical order of operations (PEMDAS):
- Multiplication/division precedence over addition/subtraction
- Exponentiation precedence over multiplication/division
- Left-to-right evaluation for same precedence
- Parentheses overriding precedence
- Complex mixed expressions
- Right associativity of exponentiation

## Test Coverage

**Total Tests: 177**
- All tests pass successfully
- Covers positive test cases (expected behavior)
- Covers negative test cases (error conditions)
- Includes edge cases and boundary conditions
- Tests mathematical accuracy with appropriate precision

## Test Framework

- Uses xUnit testing framework (.NET standard)
- Parameterized tests with Theory/InlineData attributes
- Proper error assertion with exception type checking
- Floating-point comparisons with appropriate precision tolerance

## Running Tests

```bash
cd Tests/CalculatorApp.Tests
dotnet test
```

## Test Design Principles

1. **Focused**: Each test file covers a specific area of functionality
2. **Comprehensive**: Tests cover both happy path and error conditions
3. **Isolated**: Tests are independent and can run in any order
4. **Clear**: Test names clearly describe what is being tested
5. **Accurate**: Tests reflect actual calculator behavior, not assumptions