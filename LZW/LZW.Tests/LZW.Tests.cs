namespace LZW.Tests;

using Compressor;
using Newtonsoft.Json.Serialization;

public class LZWTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("../../../TestFiles/a.txt")]
    [TestCase("../../../TestFiles/file")]
    public void OriginalAndDecompressedFileShouldBeEqual(string path)
    {
        var coefficients = new double[2];
        bool usingBwt = false;
        for (int i = 0; i < 2; ++i)
        {
            var expected = File.ReadAllBytes(path);
            System.IO.File.Copy(path, path + ".bkp");
            coefficients[i] = LZW.Compress(path, usingBwt);
            string zippedPath = path + ".zipped";
            LZW.Decompress(zippedPath);
            var afterDecompression = File.ReadAllBytes(path);
            File.Delete(path);
            File.Delete(zippedPath);
            System.IO.File.Move(path + ".bkp", path);
            File.Delete(path + ".bkp");

            Assert.That(expected, Is.EqualTo(afterDecompression));
            usingBwt = true;
        }
        Console.WriteLine($"При использовании преобразования алгоритм сжимает файл в {coefficients[1] / coefficients[0]} раз лучше");
    }

    [TestCase("../../../TestFiles/NonExistentFile")]
    public void ExceptionShouldBeThrownIfThereIsNoSuchFile_Compression(string path)
    {
        Assert.Throws<FileNotFoundException>(() => LZW.Compress(path));
    }

    [TestCase("../../../TestFiles/NonExistentFile")]
    public void ExceptionShouldBeThrownIfThereIsNoSuchFile_Decompression(string path)
    {
        Assert.Throws<FileNotFoundException>(() => LZW.Decompress(path));
    }

    [TestCase("../../../TestFiles/empty")]
    public void ExceptionShouldBeThrownIfFileIsEmpty_Compression(string path)
    {
        Assert.Throws<InvalidDataException>(() => LZW.Compress(path));
    }

    [TestCase("../../../TestFiles/empty")]
    public void ExceptionShouldBeThrownIfFileIsEmpty_Decompression(string path)
    {
        Assert.Throws<InvalidDataException>(() => LZW.Decompress(path));
    }
}
