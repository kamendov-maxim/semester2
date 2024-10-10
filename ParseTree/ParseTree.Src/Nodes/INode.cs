using System.Data;

namespace ParseTree.Dependencies;

internal interface INode
{
    double Eval();
    string ToString();

    INode? LeftChild { get; set; }
    INode? RightChild { get; set; }
}