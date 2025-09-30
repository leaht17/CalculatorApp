# CalculatorApp

## Order of Operations

CalculatorApp evaluates expressions using the following order of operations (from highest to lowest precedence):

1. **Parentheses `()`**
   - Expressions inside parentheses are evaluated first.
   - Nested parentheses are supported and evaluated from innermost to outermost.
2. **Exponentiation `^`**
   - Exponentiation is evaluated next.
   - Exponentiation is right-associative (e.g., `2 ^ 3 ^ 2` is evaluated as `2 ^ (3 ^ 2)`).
3. **Negation (unary minus `-`)**
   - Negation is applied after exponentiation.
4. **Multiplication `*` and Division `/`**
   - These operations are evaluated after exponentiation and negation.
   - They are left-associative and evaluated from left to right.
5. **Addition `+` and Subtraction `-`**
   - These are evaluated last.
   - They are left-associative and evaluated from left to right.

### Example

Expression: `-4^0.5 + 3 * 2`

Order of evaluation:
1. Exponentiation: `4^0.5` -> `2`
2. Negation: `-(2)` -> `-2`
3. Multiplication: `3 * 2` -> `6`
4. Addition: `-2 + 6` -> `4`

**Note:**
- To calculate the square root of a negative number, use parentheses: `(-4)^0.5`.
- Without parentheses, negation is applied after exponentiation.

### Supported Operations
- Parentheses: `(`, `)`
- Exponentiation: `^`
- Negation (unary minus): `-`
- Multiplication: `*`
- Division: `/`
- Addition: `+`
- Subtraction: `-`

CalculatorApp strictly follows this order to ensure correct mathematical evaluation of expressions.