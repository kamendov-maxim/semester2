namespace Stack;


/// <summary>
/// Реализация IStack с помощью списков
/// </summary>
public class ListStack : IStack
{
    public ListStack()
    {
        this.list = new List<double>();
    }

        public void Add(double element)
    {
        this.list.Add(element);
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
        }
        return Tuple.Create(value, notEmpty);
    }

    public int Size()
    {
        return list.Count;
    }

    private List<double> list;
}
