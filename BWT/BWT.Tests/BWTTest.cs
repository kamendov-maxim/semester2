namespace BWT.Tests;
using Burrows_Wheeler;
using NuGet.Frameworks;

public class Tests
{
    [TestCase("banana", "nnbaaa", 3)]
    [TestCase("vfvvffdv", "ffvvvvfd", 5)]
    public void CheckIfTransformIsCorrect(string input, string expectedResult, int expectedBwtPosition)
    {
        (string? result, int BWTPosition) = BWT.Transform(input);
        Assert.That(result, Is.EqualTo(expectedResult));
        Assert.That(BWTPosition, Is.EqualTo(expectedBwtPosition));
    }

    [TestCase("banana", "nnbaaa", 3)]
    [TestCase("vfvvffdv", "ffvvvvfd", 5)]
    public void CheckIfRevertIsCorrect(string expectedResult, string input, int BWTPosition)
    {
        string? result = BWT.Revert(input, BWTPosition);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    public void CheckIfProgramIsCorrectOnNuLLStrings()
    {
        Assert.Throws(Is.TypeOf<ArgumentNullException>(), () => BWT.Transform(""));
        Assert.Throws(Is.TypeOf<ArgumentNullException>(), () => BWT.Revert("", 1));
    }
}
