using System.ComponentModel;
using System.Formats.Asn1;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Immutable;

namespace DataStructures;

/// <summary>
/// An Implementation of skip list generic collection
/// </summary>
/// <typeparam name="T">Type supporting IComparable interface (needed for sorted structure of skiplist)</typeparam>
public class SkipList<T> : IList<T> where T : IComparable<T>
{
    private Node head;
    private readonly Node firstLevelHead;
    public int layers = 1;
    private readonly Random rnd;

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    public bool IsReadOnly { get; private set; } = false;
    private int Version = 0;

    /// <summary>
    /// Create a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        head = new(default, null, null);
        firstLevelHead = head;
        rnd = new();
    }

    /// <summary>
    /// Create a new instance of the <see cref="SkipList{T}"/> class containing elements from array
    /// </summary>
    /// <param name="array">Elements to add to a skiplist</param>
    public SkipList(T[] array)
    {
        head = new(default, null, null);
        firstLevelHead = head;
        rnd = new();
        var currentNode = head;
        Array.Sort(array);
        foreach (T item in array)
        {
            currentNode.Next = new Node(item, null, null);
            currentNode = currentNode.Next;
            ++Count;
        }
        for (int i = array.Length; i > 2; i /= 2)
        {
            var currentPreviousLevelNode = head;
            head = new Node(default, null, head);
            currentNode = head;
            while (currentPreviousLevelNode.Next is not null && currentPreviousLevelNode.Next.Next is not null)
            {
                currentNode.Next = new Node(currentPreviousLevelNode.Next.Next.Value, null, currentPreviousLevelNode.Next.Next);
                currentNode = currentNode.Next;
                currentPreviousLevelNode = currentPreviousLevelNode.Next.Next;
            }
        }
    }


    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }

            var currentNode = firstLevelHead;

            int i = -1;
            for (; i < index && currentNode is not null; ++i)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode is null || currentNode.Value is null)
            {
                throw new InvalidOperationException("List is not working correctly.");
            }
            return currentNode.Value;
        }
        set
        {
            throw new NotSupportedException("This functionality of IList is not supported in SkipList");
        }
    }
    private class Node(T? value, SkipList<T>.Node? next, SkipList<T>.Node? down)
    {
        public T? Value = value;
        public Node? Next = next;
        public Node? Down = down;
    }

    /// <inheritdoc/>
    public void Add(T value)
    {
        ++Version;
        Node? result = AddRecursion(head, value);
        if (result is not null)
        {
            head = new Node(default, new Node(value, null, result), head);
            ++layers;
        }
    }

    private Node? AddRecursion(Node currentNode, T value)
    {
        while (currentNode.Next is not null && value.CompareTo(currentNode.Next.Value) > 0)
        {
            currentNode = currentNode.Next;
        }
        Node? down = currentNode.Down == null ? null : AddRecursion(currentNode.Down, value);
        if (down is not null || currentNode.Down is null)
        {
            Count += currentNode.Down is null ? 1 : 0;
            currentNode.Next = new Node(value, currentNode.Next, down);
            return rnd.Next(2) == 1 ? currentNode.Next : null;
        }
        return null;
    }

    /// <inheritdoc/>
    public int IndexOf(T value)
    {
        var currentNode = firstLevelHead;
        int i = -1;
        while (currentNode.Next is not null && value.CompareTo(currentNode.Next.Value) >= 0)
        {
            currentNode = currentNode.Next;
            ++i;
            if (value.CompareTo(currentNode.Value) == 0)
            {
                return i;
            }
        }

        return -1;
    }

    private Node? FindNode(Node currentNode, T value)
    {
        while (currentNode.Next is not null && value.CompareTo(currentNode.Next.Value) >= 0)
        {
            currentNode = currentNode.Next;
            if (value.CompareTo(currentNode.Value) == 0)
            {
                return currentNode;
            }
        }

        return currentNode.Down is not null ? FindNode(currentNode.Down, value) : null;
    }

    /// <inheritdoc/>
    public bool Contains(T item)
    {
        return FindNode(head, item) != null;
    }

    /// <inheritdoc/>
    public void Insert(int index, T value)
    {
        throw new NotImplementedException("The method or operation is not implemented.");
    }


    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        var value = this[index];
        Remove(value);
    }

    /// <inheritdoc/>
    public bool Remove(T value)
    {
        ++Version;
        return RemoveRecursion(head, value);
    }

    private bool RemoveRecursion(Node node, T value)
    {
        bool answer = false;
        while (node.Next is not null && value.CompareTo(node.Next.Value) > 0)
        {
            node = node.Next;
        }
        if (node.Down is not null)
        {
            answer = RemoveRecursion(node.Down, value);
        }
        if (node.Next is not null && value.CompareTo(node.Next.Value) == 0)
        {
            node.Next = node.Next.Next;
            answer = true;
            Count -= node.Down is null ? 1 : 0;
        }
        return answer;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        ++Version;
        head = new Node(default, null, null);
        Count = 0;
        layers = 1;
    }

    /// <inheritdoc/>
    public T[] ToArray()
    {
        var answer = new T[Count];
        var currentNode = firstLevelHead;
        int i = 0;
        while (currentNode.Next is not null)
        {
            currentNode = currentNode.Next;
            if (currentNode.Value is null)
            {
                throw new InvalidOperationException("List is not working correctly.");
            }
            answer[i++] = currentNode.Value;
        }

        return answer;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int count)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (count + Count > array.Length)
        {
            throw new IndexOutOfRangeException("Array is too short for all skip list elements to be put into it.");
        }
        var currentNode = firstLevelHead;
        for (int i = 0; i < Count; ++i)
        {
            currentNode = currentNode.Next;
            if (currentNode is null || currentNode.Value is null)
            {
                throw new InvalidOperationException("List is not working correctly.");
            }
            array[count + i] = currentNode.Value;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return new SkipListEnumerator(this);
    }

    private class SkipListEnumerator(SkipList<T> skipList) : IEnumerator<T>
    {
        readonly SkipList<T> skipList = skipList;
        private Node CurrentNode = skipList.firstLevelHead;
        private readonly int OriginalVersion = skipList.Version;

        /// <inheritdoc/>
        public object Current
        {
            get
            {
                if (CurrentNode is null || CurrentNode.Value is null)
                {
                    throw new InvalidOperationException("List is not working correctly.");
                }
                return CurrentNode.Value;
            }
        }

        T IEnumerator<T>.Current
        {
            get
            {
                if (CurrentNode is null || CurrentNode.Value is null)
                {
                    throw new InvalidOperationException("List is not working correctly.");
                }
                return CurrentNode.Value;
            }
        }

        /// <inheritdoc/>
        public void Dispose() => GC.SuppressFinalize(this);

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (CurrentNode.Next == null)
            {
                return false;
            }

            if (OriginalVersion != skipList.Version)
            {
                throw new InvalidOperationException("List is modified");
            }

            CurrentNode = CurrentNode.Next;

            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            CurrentNode = skipList.firstLevelHead;
        }
    }
}
