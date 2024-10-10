using System.Diagnostics;

namespace Stack;

/// <summary>
/// Реализация IStack с помощью встроенного класса Node
/// </summary>
public class StackWithNodes : IStack
{
    private class Node
    {
        public Node()
        {
            this.value = 0;
        }

        public double value;
        public Node? next;
    }

    public StackWithNodes()
    {
        top = new Node();
    }


    public void Add(double element)
    {
        Node newNode = new Node();
        newNode.next = this.top;
        newNode.value = element;
        this.top = newNode;
        ++this.count;
    }

    public Tuple<double, bool> Pop()
    {
        double answer = -1.0;
        bool notEmpty = false;
        if (this.count > 0)
        {
            Node next = this.top.next;
            answer = this.top.value;
            this.top = next;
            notEmpty = true;
            --this.count;
        }
        return Tuple.Create(answer, notEmpty);
    }

    public int Size()
    {
        return this.count;
    }

    public int count { get; private set; }
    private Node top;
}
