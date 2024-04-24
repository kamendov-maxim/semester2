namespace ParseTree.Tests;

using System.Data;
using ParseTree;

public class Tests
{
    [TestCase("(  * (+ 1 1 ) 2 )", 4)]
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

    [TestCase("(* (+ 1 1) 2)")]
    [TestCase("5")]
    [TestCase("(/ 5 2)")]
    public void ToStringTests(string expression)
    {
        var tree = new Tree(expression);
        var result = tree.ToString();

        Assert.That(result, Is.EqualTo(expression));
    }
}
 