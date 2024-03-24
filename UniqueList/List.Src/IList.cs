namespace Lists;

public interface IList
{
    void Add(int value);
    void Insert(int index, int value);
    int[] ToArray();
    int RemoveAt(int index);
    int FindValue(int index);
    int Count { get; }
}
