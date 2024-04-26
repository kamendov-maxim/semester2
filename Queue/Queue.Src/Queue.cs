using System.Security;

namespace DataStructures;

public class Queue<T>(int startCapacity = 1)
{
    private Tuple<T, int>[] heap = new Tuple<T, int>[startCapacity];
    public int CurrentCapacity = startCapacity;
    public int Count = -1;

    public void Enqueue(T value, int priority)
    {
        if (Empty())
        {
            throw new QueueIsEmtpyException("Queue is empty");
        }
        if (Count == CurrentCapacity - 1)
        {
            IncreaseCapacity();
        }

        ++Count;
        heap[Count] = new Tuple<T, int>(value, priority);

        MoveUp(Count);
    }

    public T Dequeue()
    {
        T result = heap[0].Item1;
        heap[0] = heap[Count--];
        MoveDown(0);
        return result;
    }

    public bool Empty() => Count == 0;

    private void IncreaseCapacity()
    {
        CurrentCapacity *= 2;
        Array.Resize(ref heap, CurrentCapacity);
    }

    private void MoveUp(int i)
    {
        int parent = ParentIndex(i);
        while (i > 0 && heap[parent].Item2 < heap[i].Item2)
        {
            Swap(parent, i);
            i = parent;
            parent = ParentIndex(i);
        }
    }

    private void MoveDown(int i)
    {
        int current = i;
        int left = LeftChildIndex(current);
        current = left <= Count && heap[left].Item2 > heap[current].Item2 ? left : current;
        int right = RightChildIndex(current);
        current = right <= Count && heap[right].Item2 > heap[current].Item2 ? right : current;

        if (i != current)
        {
            Swap(current, i);
            MoveDown(current);
        }
    }

    private void Swap(int a, int b) => (heap[b], heap[a]) = (heap[a], heap[b]);
    private static int ParentIndex(int index) => (index - 1) / 2;
    private static int LeftChildIndex(int index) => (2 * index) + 1;
    private static int RightChildIndex(int index) => (2 * index) + 2;
}
