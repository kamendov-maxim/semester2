using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using System.Xml;

namespace Lists;

/// <summary>
/// An implementation of linked list
/// </summary>
public class List: IList
{
    protected class ListElement
    {
        public ListElement? Next;
        public ListElement? Previous;
        public int Value;
        public ListElement(int value)
        {
            Value = value;

            Next = null;
            Previous = null;
        }
    }

    public List()
    {
        Count = 0;
    }

    /// <summary>
    /// Method to insert value at given index
    /// </summary>
    /// <param name="index">Where to insert value</param>
    /// <param name="value">Value to insert</param>
    /// <exception cref="OutOfRangeException">Exception is thrown if index is less than 0 or
    /// bigger than list.Count and it is impossible to insert value</exception>
    public virtual void Insert(int index, int value)
    {
        if (index > Count || index < 0)
        {
            throw new OutOfRangeException();
        }

        var newElement = new ListElement(value);

        if (index == Count)
        {
            if (Count == 0)
            {
                head = newElement;
            }
            if (bottom is not null)
            {
                newElement.Previous = bottom;
                bottom.Next = newElement;
            }
            bottom = newElement;
            ++Count;
            return;
        }

        if (index == 0)
        {
            newElement = new ListElement(value);
            head.Previous = newElement;
            newElement.Next = head;
            head = newElement;
            ++Count;
            return;
        }

        var currenElement = FindNode(index);
        newElement.Next = currenElement;
        newElement.Previous = currenElement.Previous;
        currenElement.Previous.Next = newElement;
        currenElement.Previous = newElement;
        ++Count;
    }

    /// <summary>
    /// Adds value to the end of list
    /// </summary>
    /// <param name="value">Value to add</param>
    public virtual void Add(int value)
    {
        var newElement = new ListElement(value);
        if (Count == 0)
        {
            head = newElement;
            bottom = newElement;
        }
        else if (Count == 1)
        {
            head.Next = newElement;
            newElement.Previous = head;
        }
        else
        {
            bottom.Next = newElement;
            newElement.Previous = bottom;
        }
        bottom = newElement;
        ++Count;
    }

    /// <summary>
    /// Method to get an int  array containing all the values from list in the same order
    /// </summary>
    /// <returns>Array with values</returns>
    public int[] ToArray()
    {
        var output = new int[Count];
        var currenElement = head;
        for (int i = 0; i < Count; ++i)
        {
            output[i] = currenElement.Value;
            currenElement = currenElement.Next;
        }

        return output;
    }

    /// <summary>
    /// Method allowing to remove value at given index
    /// </summary>
    /// <param name="index">Index of element to remove</param>
    /// <returns>Value of this element</returns>
    /// <exception cref="OutOfRangeException">Thrown if there is no element with such index in the list</exception>
    public int RemoveAt(int index)
    {
        if (index > Count - 1 || index < 0 || Count == 0)
        {
            throw new OutOfRangeException();
        }

        if (index == Count - 1)
        {
            if (Count == 0)
            {
                head = null;
                bottom = null;
            }
            else
            {
                bottom = bottom.Previous;
            }
        }

        int value;
        
        if (index == 0)
        {
            value = head.Value;
            head = head.Next;
            --Count;
            return value;
        }

        var element = FindNode(index);
        value = element.Value;
        element.Next.Previous = element.Previous;
        element.Previous.Next = element.Next;
        --Count;
        return value;
    }

    /// <summary>
    /// Method to get a value of an element at a givent index
    /// </summary>
    /// <param name="index">Index of an element to find</param>
    /// <returns>Value of an element</returns>
    /// <exception cref="OutOfRangeException">Thrown if there is no element with such index in the list</exception>
    public int FindValue(int index)
    {
        if (index > Count - 1 || index < 0)
        {
            throw new OutOfRangeException("Index is out of range");
        }

        var element = FindNode(index);

        return element.Value;
    }

    protected ListElement FindNode(int index)
    {
        ListElement? currenElement;
        if (index < Count / 2)
        {
            currenElement = head;
            for (int i = 0; i < index; ++i)
            {
                currenElement = currenElement.Next;
            }
        }
        else
        {
            currenElement = bottom;
            for (int i = Count - 1; i > index; --i)
            {
                currenElement = currenElement.Previous;
            }
        }

        return currenElement;
    }

    public int Count {get; private set; }
    protected ListElement? head = null;
    protected ListElement? bottom = null;
}
