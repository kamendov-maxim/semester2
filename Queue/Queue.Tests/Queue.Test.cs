namespace Queue.Tests;
using DataStructures;

public class Tests
{
    private readonly int testSize = 6;
    private readonly int[] priorities = [1, 2, 6, 3, 8, 4];
    private readonly string[] values = ["one", "two", "six", "three", "eight", "four"];
    private readonly string[] answers = ["eight", "six", "four", "three", "two", "one"];

    [Test]
    public void TestQueue()
    {
        var queue = new DataStructures.Queue<string>(testSize + 1);
        for (int i = 0; i < testSize; ++i)
        {
            queue.Enqueue(values[i], priorities[i]);
        }
        for (int i = 0; i < testSize; ++i)
        {
            Assert.That(queue.Dequeue, Is.EqualTo(answers[i]));
        }
    }
}