using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using Stack;

namespace CalculatorNamespace;

public class Calculator
{
    public static Tuple<double, ErrorCode> Evaluate(string expression, IStack stack)
    {
        if (expression == null)
        {
            return Tuple.Create(-1.0, ErrorCode.ExpressionIsNull);
        }

        Array elements = expression.Split(" ");
        foreach (string item in elements)
        {
            if (int.TryParse(item, out int number))
            {
                stack.Add((double)number);
            }
            else
            {
                if (item.Length != 1)
                {
                    return Tuple.Create(-1.0, ErrorCode.WrongOperator);
                }
                (double number2, bool notEmpty2) = stack.Pop();
                if (notEmpty2)
                {
                    (double number1, bool notEmpty1) = stack.Pop();
                    if (notEmpty1)
                    {
                        (double result, ErrorCode ec) = Operation(number1, number2, item[0]);
                        if (ec != ErrorCode.Ok)
                        {
                            return Tuple.Create(-1.0, ec);
                        }
                        stack.Add(result);
                    }

                }
            }
        }
        (double answer, bool notEmpty) = stack.Pop();
        return Tuple.Create(answer, ErrorCode.Ok);
    }

    private static Tuple<double, ErrorCode> Operation(double number1, double number2, char op)
    {
        switch (op)
        {
            case '+':
                {
                    return Tuple.Create(number1 + number2, ErrorCode.Ok);
                }
            case '-':
                {
                    return Tuple.Create(number1 - number2, ErrorCode.Ok);
                }
            case '/':
                {
                    if (Math.Abs(number2) < 0.0000001)
                    {
                        return Tuple.Create(0.0, ErrorCode.DivisionByZero);
                    }
                    return Tuple.Create(number1 / number2, ErrorCode.Ok);
                }
            case '*':
                {
                    return Tuple.Create(number1 * number2, ErrorCode.Ok);
                }
            default:
                return Tuple.Create(-1.0, ErrorCode.WrongOperator);
        }
    }

    private static void ClearStack(IStack stack)
    {
        while (stack.Size > 0)
        {
            stack.Pop();
        }
    }

    public enum ErrorCode
    {
        Ok,
        DivisionByZero,
        WrongOperator,
        ExpressionIsNull
    }

}
