namespace ParseTree.Dependencies;

internal class NumberNode : INode
{
    public double Eval()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public NumberNode(int value)
    {
        Value = value;
        LeftChild = null;
    }

    public INode? LeftChild { get; set; }
    public INode? RightChild { get; set; }
    public double Value;
}
