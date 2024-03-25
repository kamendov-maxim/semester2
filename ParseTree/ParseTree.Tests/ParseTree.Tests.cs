namespace ParseTree.Tests;

using System.Data;
using ParseTree;

public class Tests
{
    [TestCase("(* (+ 1 1) 2)", 4)]
    [TestCase("5", 5)]
    [TestCase("(/ 5 2)", 2.5)]
    public void TestEvaluation(string expression, double expectedResult)
    {
        var tree = new Tree(expression);
        double result = tree.Evaluate();

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("")]
    [TestCase("]")]
    [TestCase("7 7")]
    [TestCase("+ 5 5")]
    public void IncorrectInputTest(string expression)
    {
        Assert.Throws<IncorrectExpressionException>(() => new Tree(expression));
    }
}
 