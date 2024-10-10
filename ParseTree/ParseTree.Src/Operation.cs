using System.Runtime.InteropServices;

namespace ParseTree.Dependencies;

/// <summary>
/// Class implementing arithmetic operations based on char input
/// </summary>
class Operation
{
    /// <summary>
    /// Do the arithmetic operation corresponding to char
    /// </summary>
    /// <param name="operation">Char '*', '+', '-' or '/'</param>
    /// <param name="operandLeft">Left operand</param>
    /// <param name="operandRight">Right operand</param>
    /// <returns>Result of the operation</returns>
    /// <exception cref="DivideByZeroException">Thrown if operandRight is 0 and operation is '/'</exception>
    /// <exception cref="IncorrectExpressionException">thrown if argument operation is something different to '*', '+', '-' or '/'</exception>
    public static double Calculate(char operation, double operandLeft, double operandRight)
    {
        switch (operation)
        {
            case '+':
                {
                    return operandLeft + operandRight;
                }
            case '-':
                {
                    return operandLeft - operandRight;
                }
            case '*':
                {
                    return operandLeft * operandRight;
                }
            case '/':
                {
                    if (operandRight == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return operandLeft / operandRight;
                }
            default:
                {
                    throw new IncorrectExpressionException();
                }
        }
    }
}
