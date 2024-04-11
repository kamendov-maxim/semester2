// copyright file: https://github.com/kamendov-maxim/semester2/blob/master/LICENSE
namespace Vector.Tests;

using Vector;

public class Tests
{
    readonly int[] testArray1 = [0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 0, 0, 0, 3, 4, 0, 5];
    readonly int[] testArray2 = [1, 0, 3, 0, 0, 5, 0, 0, 0, 0, 0, 6, 0, 5, 2, 0, 0];
    readonly int[] sub = [-1, 0, -3, 0, 0, -4, 0, 0, 2, 0, 0, -6, 0, -2, 2, 0, 5];
    readonly int MultiplicationAnswer = 28;
    readonly int[] testArray3 = [0, 0, 0, 0, 0, 1, 0, 0, 2];
    readonly int[] testZeroArray1 = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    readonly int[] testZeroArray2 = [0, 0, 0, 0, 0, 0, 0, 0];

    [Test]
    public void TestCreateInstanceAndGetArrayWithoutChanges()
    {
        var vector = new SparseVector(testArray1);
        var newArray = vector.ToArray();
        Assert.That(newArray, Is.EqualTo(testArray1));
    }

    [Test]
    public void TestSummation()
    {
        var vector = new SparseVector(testArray1);
        var secondVector = new SparseVector(testArray2);
        vector.Sum(secondVector);
        var array = vector.ToArray();

        int[] answer = [1, 0, 3, 0, 0, 6, 0, 0, 2, 0, 0, 6, 0, 8, 6, 0, 5];
        Assert.That(array, Is.EqualTo(answer));
    }

    [Test]
    public void TestSubstraction()
    {
        var vector = new SparseVector(testArray1);
        var secondVector = new SparseVector(testArray2);
        vector.Substract(secondVector);
        var array = vector.ToArray();

        int[] answer = [-1, 0, -3, 0, 0, -4, 0, 0, 2, 0, 0, -6, 0, -2, 2, 0, 5];
        Assert.That(array, Is.EqualTo(answer));
    }

    [Test]
    public void TestMultiplication()
    {
        var vector = new SparseVector(testArray1);
        var secondVector = new SparseVector(testArray2);
        var result = vector.ScalarMultiplicationWith(secondVector);

        int answer = 28;
        Assert.That(result, Is.EqualTo(answer));
    }

    [Test]
    public void ZeroVectorCases()
    {
        var vector = new SparseVector(testArray1);
        var secondVector = new SparseVector(testZeroArray1);
        vector.Sum(secondVector);
        var array = vector.ToArray();
        Assert.That(array, Is.EqualTo(testArray1));
        vector.Substract(secondVector);
        array = vector.ToArray();
        Assert.That(array, Is.EqualTo(testArray1));
        int MultiplicationResult = vector.ScalarMultiplicationWith(secondVector);
        Assert.That(MultiplicationResult, Is.EqualTo(0));
    }

    [Test]
    public void TestThatThrowsException()
    {
        var vector = new SparseVector(testArray1);
        var secondVector = new SparseVector(testArray3);
        Assert.Throws<DifferentLengthVectorsException>(() => vector.Sum(secondVector));
        Assert.Throws<DifferentLengthVectorsException>(() => vector.Substract(secondVector));
        Assert.Throws<DifferentLengthVectorsException>(() => vector.ScalarMultiplicationWith(secondVector));
    }
}