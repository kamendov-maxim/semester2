using Microsoft.VisualBasic;

namespace Stack;

interface IStack
{
    void Add(double element);

    Tuple<double, bool> Pop();

    int Size { get; }
}
