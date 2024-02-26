using System.Diagnostics;

namespace Stack;

class StackWithNodes : IStack
{
    private class Node
    {
        public Node()
        {
            this.value = 0;
        }

        public double value;
        public Node next;
    }


    public void Add(double element)
    {
        Node newNode = new Node();
        newNode.next = this.top;
        newNode.value = element;
        this.top = newNode;
        ++this.Size;
    }

    public Tuple<double, bool> Pop()
    {
        double answer = -1.0;
        bool notEmpty = false;
        if (this.Size > 0)
        {
            Node next = this.top.next;
            answer = this.top.value;
            this.top = next;
            notEmpty = true;
            --this.Size;
        }
        return Tuple.Create(answer, notEmpty);
    }

    public int Size { get; private set; }
    private Node? top;
}
