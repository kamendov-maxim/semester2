namespace BubbleSort.Tests;
using finalTest1;

public class Tests
{
    private readonly int[] intArray = [6, 3, 89, 54, 2, 5, 2, 0, 1, 4, 7, 34, 76, 2, 6];
    private readonly string[] stringArray = ["ddd", "aa", "c", "bbbb"];
    private readonly List<List<int>> listOfLists = [[2, 2], [1], [4, 4, 4, 4], [3, 3, 3, 3]];

    [Test]
    public void TestBubbleSortWithStringsAndComparerByLength()
    {
        string[] expectedResult = ["c", "aa", "ddd", "bbbb"];
        Sorting.BubbleSort(stringArray, Comparer<string>.Create((string a, string b) => a.Length - b.Length));
        Assert.That(stringArray, Is.EqualTo(expectedResult));
    }

    public void TestBubbleSortOnListOfLIsts()
    {
        List<List<int>> expectedResult = [[1], [2, 2], [3, 3, 3, 3], [4, 4, 4, 4]];
        Sorting.BubbleSort<List<int>>(listOfLists, Comparer<List<int>>.Create((List<int> a, List<int> b) => a.Count - b.Count));
        Assert.That(listOfLists, Is.EqualTo(expectedResult));

    }

    [Test]
    public void TestBubbleSortWithIntsAndDefaultComparer()
    {
        int[] expectedResult = [0, 1, 2, 2, 2, 3, 4, 5, 6, 6, 7, 34, 54, 76, 89];
        Sorting.BubbleSort(intArray, Comparer<int>.Default);
        Assert.That(intArray, Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestBubbleSortWIthStringsAndDefaultComparer()
    {
        string[] expectedResult = ["aa", "bbbb", "c", "ddd"];
        Sorting.BubbleSort(stringArray, Comparer<string>.Default);
        Assert.That(stringArray, Is.EqualTo(expectedResult));
    }
}