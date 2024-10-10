namespace Calculator.Tests;

using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CalculatorNamespace;
using Stack;

public class Tests
{
    public static readonly string[] expressions = ["5 7 +", "5 -7 +", "5 0 +",
    "5 25 *", "7 -15 *", "-10 -14 *", "-10 0 *", "6 0 /", "10 2 /", "-5 2 /",
    "dfsgfvx", ""];
    private static readonly double[] expectedAnswers = [12.0, -2.0, 5.0, 125.0, -105.0,
    140.0, 0.0, -1, 5.0, -2.5, -1, -1];
    private static readonly Calculator.ErrorCode[] expectedErrorCodes =
    [Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.DivisionByZero,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.Ok,
    Calculator.ErrorCode.WrongOperator,
    Calculator.ErrorCode.ExpressionIsNull];

    private static IEnumerable<(string, double, Calculator.ErrorCode, IStack)> InputData
    {
        get
        {
            for (int i = 0; i < expressions.Length; ++i)
            {
                yield return new(expressions[i], expectedAnswers[i], expectedErrorCodes[i], new ListStack());
                yield return new(expressions[i], expectedAnswers[i], expectedErrorCodes[i], new StackWithNodes());
            }
        }
    }

    [TestCaseSource(nameof(InputData))]
    public void CalculationTest((string, double, Calculator.ErrorCode, IStack) inputData)
    {
        (var expression, var expectedAnswer, var errorCode, var stack) = inputData;
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(ec, Is.EqualTo(errorCode));
            if (errorCode == Calculator.ErrorCode.Ok)
            {
                Assert.That(answer, Is.EqualTo(expectedAnswer).Within(double.Epsilon));
            }
        });
    }
}