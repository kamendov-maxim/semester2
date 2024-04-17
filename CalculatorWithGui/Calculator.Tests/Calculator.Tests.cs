namespace CalculatorWithGui.Tests;
using CalculatorAlgorithm;

public class CalculatorAlgorithmTests
{
    [TestCase( new char[] {'2', '+', '3', '±', '-', '5', '±', '/', '2', '='}, new string[] {"2", "2+", "2+3", "2+-3", "-1-", "-1-5", "-1--5", "4/", "4/2", "2"})]
    [TestCase(new char[] {'2', '+', '2', '/', '0', '=', 'C', '3', '-', '1', '='}, new string[] {"2", "2+", "2+2", "4/", "4/0", "Error", "0", "3", "3-", "3-1", "2"})]
    public void Test1(char[] test, string[] answers)
    {
        Calculator inputHandler = new();
        for (int i = 0; i < test.Length; ++i)
        {
            Assert.That(inputHandler.NextStep(test[i]), Is.EqualTo(answers[i]));
        }
        Assert.Pass();
    }
}