namespace Stack;


class ListStack : IStack
{
    public ListStack()
    {
        this.Size = 0;
        this.list = new List<double>();
    }

    public void Add(double element)
    {
        this.list.Add(element);
        ++this.Size;
    }

    public Tuple<double, bool> Pop()
    {
        double value = -1;
        bool notEmpty = false;
        if (this.list.Count > 0)
        {
            notEmpty = true;
            value = this.list[this.list.Count - 1];
            this.list.RemoveAt(this.list.Count - 1);
            --this.Size;
        }
        return Tuple.Create(value, notEmpty);
    }

    private List<double> list;
    public int Size { get; private set; }
}