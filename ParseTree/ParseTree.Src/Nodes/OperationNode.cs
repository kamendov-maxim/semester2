namespace ParseTree.Dependencies;

internal class OperationNode : INode
{
    public INode? LeftChild { get; set; }
    public INode? RightChild { get; set; }
    public char operation;

    public OperationNode(char operationChar)
    {
        operation = operationChar;
    }

    public double Eval()
    {
        if (LeftChild is not null && RightChild is not null)
        {
            return Operation.Calculate(operation, LeftChild.Eval(), RightChild.Eval());
        }
        throw new IncorrectTreeException();
    }

    public override string ToString()
    {
        if (LeftChild is not null && RightChild is not null)
        {
            return $"( {operation} {LeftChild.ToString()} {RightChild.ToString()})";
        }
        throw new IncorrectTreeException();
    }
}
