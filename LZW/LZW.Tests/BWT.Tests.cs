namespace LZW.Tests;

using LZW.Dependencies;

public class BWTTests
{
    [TestCase(new byte[] {98, 97, 110, 97, 110, 97}, new byte[] {110, 110, 98, 97, 97, 97}, 3)]
    [TestCase(new byte[] {98, 108, 97, 103, 114, 114, 98, 105, 97, 114, 107, 100, 102, 106, 104, 97, 102, 100}, new byte[] {104, 108, 105, 114, 100, 102, 107, 97, 100, 97, 106, 98, 102, 114, 98, 114, 97, 103}, 4)]
    public void CheckTransform(byte[] input, byte[] expectedResult, int expectedBWTPosition)
    {
        (byte[] result, int BWTPosition) = BWT.Transform(input);

        Assert.That(result, Is.EqualTo(expectedResult));
        Assert.That(BWTPosition, Is.EqualTo(expectedBWTPosition));
    }

    [TestCase(new byte[] {98, 97, 110, 97, 110, 97}, new byte[] {110, 110, 98, 97, 97, 97}, 3)]
    [TestCase(new byte[] {98, 108, 97, 103, 114, 114, 98, 105, 97, 114, 107, 100, 102, 106, 104, 97, 102, 100}, new byte[] {104, 108, 105, 114, 100, 102, 107, 97, 100, 97, 106, 98, 102, 114, 98, 114, 97, 103}, 4)]
    public void CheckReverseTransform(byte[] expectedResult, byte[] input, int BWTPosition)
    {
        byte[] result = BWT.Revert(input, BWTPosition);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
