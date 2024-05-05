namespace List.Tests;
using Lists;

public class ListTests
{
    [Test]
    public void AddTest()
    {
        var list = new List();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        var result = list.ToArray();
        Assert.That(result, Is.EqualTo(new int[4] { 1, 2, 3, 4 }));
    }

    [Test]
    public void InsertTest()
    {
        var list = new List();
        list.Insert(0, 1);
        list.Insert(1, 2);
        list.Insert(2, 3);
        list.Insert(3, 4);
        var result = list.ToArray();
        Assert.That(result, Is.EqualTo(new int[4] { 1, 2, 3, 4 }));
        list.Insert(2, 5);
        list.Insert(4, 6);
        result = list.ToArray();
        Assert.That(result, Is.EqualTo(new int[6] { 1, 2, 5, 3, 6, 4 }));
    }

    [Test]
    public void TestOutOfRangeCase()
    {
        var list = new List();
        Assert.Throws<IndexOutOfRangeException>(() => list.Insert(10, 6));
        Assert.Throws<IndexOutOfRangeException>(() => list.Insert(-10, 6));

        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(10));
        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-10));

        Assert.Throws<IndexOutOfRangeException>(() => list.FindValue(10));
        Assert.Throws<IndexOutOfRangeException>(() => list.FindValue(-10));
    }

    [Test]
    public void TestRemove()
    {
        var list = new List();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.RemoveAt(2);
        var result = list.ToArray();
        Assert.That(result, Is.EqualTo(new int[3] {1, 2, 4}));
        list.RemoveAt(0);
        result = list.ToArray();
        Assert.That(result, Is.EqualTo(new int[2] {2, 4}));
        list.RemoveAt(1);
        result = list.ToArray();
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
        Assert.That(result, Is.EqualTo(new int[1] {2}));
    }
}
