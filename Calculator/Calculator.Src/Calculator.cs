using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using Stack;

namespace CalculatorNamespace;

/// <summary>
/// Класс, содержищий в себе метод для вычисления выражений, записанных в постфиксной записи
/// </summary>
public class Calculator
{
    /// <summary>
    /// Метод для вычисления выражений, записанных в постфиксной записи
    /// </summary>
    /// <param name="expression">Выражение в постфиксной записи</param>
    /// <param name="stack">Стек, реализующий интерфейс IStack. С помощью него калькулятор будет вычислять значение выражения </param>
    /// <returns></returns>
    public static Tuple<double, ErrorCode> Evaluate(string expression, IStack stack)
    {
        if (expression == null || expression == string.Empty)
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
                    if (Math.Abs(number2) < double.Epsilon)
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
        while (stack.Size() > 0)
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
