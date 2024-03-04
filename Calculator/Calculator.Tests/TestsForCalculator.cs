namespace Calculator.Tests;

using System.Collections.Specialized;
using System.Security.Cryptography;
using CalculatorNamespace;
using Stack;

public class Tests
{
    public double epsilon = 0.00000001;

    private static IEnumerable<IStack> stacks
    {
        get
        {
            yield return new ListStack();
            yield return new StackWithNodes();
        }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test, TestCaseSource(nameof(stacks))]
    public void PositivePlusPositiveEqualsPositive(IStack stack)
    {
        string expression = "5 7 +";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(12.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    [Test, TestCaseSource(nameof(stacks))]
    public void PositivePLusNegativeEqualsNegative(IStack stack)
    {
        string expression = "5 -7 +";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-2.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    [Test, TestCaseSource(nameof(stacks))]
    public void SumWithZero(IStack stack)
    {
        string expression = "5 0 +";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(5.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    [Test, TestCaseSource(nameof(stacks))]
    public void MultiplicationOfPositiveAndPositive(IStack stack)
    {
        string expression = "5 25 *";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(125.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    public void MultiplicationOfPositiveAndNegative(IStack stack)
    {
        string expression = "7 -15 *";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-105.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    public void MultiplicationOfNegativeAndNegative(IStack stack)
    {
        string expression = "-10 -14 *";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(140.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }
    public void MultiplicationWithZero(IStack stack)
    {
        string expression = "-10 0 *";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(0.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    public void DivisionByZero(IStack stack)
    {
        string expression = "6 0 /";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-1.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.DivisionByZero));
        });
    }

    public void DivizionOfPositiveAndPositive(IStack stack)
    {
        string expression = "10 2 /";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(5.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    public void DivizionOfPositiveAndNegative(IStack stack)
    {
        string expression = "-5 2 /";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-2.5).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.Ok));
        });
    }

    public void WrongInput(IStack stack)
    {
        string expression = "dfsgfvx";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-1.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.WrongOperator));
        });
    }

    public void EmptyString(IStack stack)
    {
        string expression = "";
        (double answer, Calculator.ErrorCode ec) = Calculator.Evaluate(expression, stack);
        Assert.Multiple(() =>
        {
            Assert.That(answer, Is.EqualTo(-1.0).Within(epsilon));
            Assert.That(ec, Is.EqualTo(Calculator.ErrorCode.ExpressionIsNull));
        });
    }
}