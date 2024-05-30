namespace NullObjects.Tests;
using Zachet;

public class Tests
{
    readonly List<int> emptyList = [];
    readonly List<int> noNullObjectsList = [1, 2, 3, 4, 5, 6, 7, 8];
    readonly List<int> listWithNulls = [1, 5, 0, 2, 4, 0, 0, 5, 2];
    readonly IntNullChecker intNullChecker = new();

    [Test]
    public void EmptyListTest()
    {
        Assert.That(NullCounter.CountNullObjects<int>(emptyList, intNullChecker), Is.EqualTo(0));
    }

    [Test]
    public void NoNullObjectsTest()
    {
        Assert.That(NullCounter.CountNullObjects<int>(noNullObjectsList, intNullChecker), Is.EqualTo(0));
    }

    [Test]
    public void ListWithNullsTest()
    {
        Assert.That(NullCounter.CountNullObjects<int>(listWithNulls, intNullChecker), Is.EqualTo(3));
    }
}
