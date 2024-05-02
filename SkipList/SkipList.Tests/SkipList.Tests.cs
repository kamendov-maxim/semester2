namespace SkipList.Tests;

using System.Runtime.Intrinsics.X86;
using DataStructures;

public class Tests
{
    private readonly int[] intTestArray = [5, 7, 3, 8, 9, 1, 0, 0, 0, 2, 10, 15, 16];
    private readonly int[] intAnswerArray = [0, 0, 1, 3, 5, 7, 9, 10, 15, 16];
    private readonly string[] stringTestArray = ["aaa", "ccc", "rrr", "eee", "hhh", "www", "xxx", "bbb"];
    private readonly string[] stringAnswerArray = ["aaa", "bbb", "ccc", "eee", "hhh", "rrr", "www", "xxx"];

    [Test]
    public void IntTest()
    {
        var skipList = new SkipList<int>(intTestArray);
        skipList.Remove(0);
        skipList.RemoveAt(3);
        skipList.Remove(8);

        var answer = skipList.ToArray();
        Assert.That(answer, Is.EqualTo(intAnswerArray));
    }

    [Test]
    public void StringTest()
    {
        var skipList = new SkipList<string>(stringTestArray);

        var answer = skipList.ToArray();
        Assert.That(answer, Is.EqualTo(stringAnswerArray));
    }

    [Test]
    public void TestClear()
    {
        var skipList = new SkipList<int>(intTestArray);
        skipList.Clear();
        Assert.That(skipList, Is.Empty);
    }

    [Test]
    public void TestCopyTo()
    {
        var skipList = new SkipList<int>(intTestArray);
        var array = new int[20];
        skipList.CopyTo(array, 4);
        for (int i = 4; i < skipList.Count; ++i)
        {
            Assert.That(array[i], Is.EqualTo(intTestArray[i - 4]));
        }

        array = new int[10];
        Assert.Throws<IndexOutOfRangeException>(() => skipList.CopyTo(array, 0));
    }

    [Test]
    public void TestContatinsAndIndexOf()
    {
        var skipList = new SkipList<int>(intTestArray);

        Assert.That(skipList.Contains(5));
        Assert.That(!skipList.Contains(6));

        Assert.That(skipList.IndexOf(5), Is.EqualTo(6));
        Assert.That(skipList.IndexOf(6), Is.EqualTo(-1));
    }

    [Test]
    public void TestNotImplementedException()
    {
        var skipList = new SkipList<string>();
        Assert.Throws<NotImplementedException>(() => skipList.Insert(5, "aaa"));
    }

    [Test]
    public void TestForeach()
    {
        var skipList = new SkipList<string>(stringTestArray);
        var array = new string[skipList.Count];
        int i = 0;
        foreach (var item in skipList)
        {
            array[i++] = item;
        }

        Assert.That(array, Is.EqualTo(stringAnswerArray));
    }

    [Test]
    public void IsReadOnlyShouldReturnFalse()
    {
        var skipList = new SkipList<int>();
        Assert.That(!skipList.IsReadOnly);
    }

    [Test]
    public void ModifyingListInForeachLoopShouldThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => AddElementWhileBeingInLoop());
        Assert.Throws<InvalidOperationException>(() => RemoveElementWhileBeingInLoop());
        Assert.Throws<InvalidOperationException>(() => ClearWhileBeingInLoop());
    }

    private void AddElementWhileBeingInLoop()
    {
        var skipList = new SkipList<int>(intTestArray);

        foreach (var item in skipList)
        {
            skipList.Add(1);
        }
    }

    private void RemoveElementWhileBeingInLoop()
    {
        var skipList = new SkipList<int>(intTestArray);

        foreach (var item in skipList)
        {
            skipList.Remove(1);
        }
    }

    private void ClearWhileBeingInLoop()
    {
        var skipList = new SkipList<int>(intTestArray);

        foreach (var item in skipList)
        {
            skipList.Clear();
        }
    }
}
