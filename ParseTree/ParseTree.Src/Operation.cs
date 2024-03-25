using System.Runtime.InteropServices;

namespace ParseTree.Dependencies;

class Operation
{
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
