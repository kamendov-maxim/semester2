namespace Routers.Tests;
using System.Runtime.InteropServices;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("../../../TestFiles/TestInput1.txt", "../../../TestFiles/TestOutput.txt")]
    public void IOTest(string inputFile, string outputFile)
    {
        RoutersGraph graph = IO.Read(inputFile);
        IO.Write(graph, outputFile);
        var expected = File.ReadAllBytes(inputFile);
        var result = File.ReadAllBytes(outputFile);
        Assert.That(result, Is.EqualTo(expected));
        File.WriteAllText(outputFile, string.Empty);
    }

    [TestCase("../../../TestFiles/TestInput1.txt", "../../../TestFiles/TestOutput.txt", "../../../TestFiles/ExpectedResult1.txt")]
    [TestCase("../../../TestFiles/TestInput2.txt", "../../../TestFiles/TestOutput.txt", "../../../TestFiles/ExpectedResult2.txt")]
    [TestCase("../../../TestFiles/TestInput3.txt", "../../../TestFiles/TestOutput.txt", "../../../TestFiles/ExpectedResult3.txt")]
    public void AlgorithmTest(string inputFile, string outputFile, string expectedFile)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            inputFile = inputFile.Replace('/', '\\');
            outputFile = outputFile.Replace('/', '\\');
            expectedFile = expectedFile.Replace('/', '\\');
        }

        Configurator.Configure(inputFile, outputFile);
        var expected = File.ReadAllBytes(expectedFile);
        var result = File.ReadAllBytes(outputFile);
        Assert.That(result, Is.EqualTo(expected));
        File.WriteAllText(outputFile, string.Empty);
    }

    [TestCase("../../../TestFiles/ExceptionTest.txt")]
    public void NetworkIsNotConnected(string inputFile)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            inputFile = inputFile.Replace('/', '\\');
        }
        Assert.Throws<NetworkIsNotConnectedException>(() => Configurator.Configure(inputFile, "a"));
    }
}
