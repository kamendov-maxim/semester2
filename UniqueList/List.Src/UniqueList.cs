using System.ComponentModel;

namespace Lists;

/// <summary>
/// Modification of List that doesn't allow you to keep more than one value with the same value
/// </summary>
public class UniqueList : List
{
    private bool Contains(int value)
    {
        if (Count == 0)
        {
            return false;
        }

        var currenElement = head;
        for (int i = 0; i < Count; ++i)
        {
            if (currenElement.Value == value)
            {
                return true;
            }
            currenElement = currenElement.Next;
        }

        return false;
    }

    /// <summary>
    /// Adds value to the end of list. Throws an exception if element is already in list
    /// </summary>
    /// <param name="value">Value to add</param>
    /// <exception cref="ElementExistsException">Thrown if this element already exists in list</exception>
    public override void Add(int value)
    {
        if (Contains(value))
        {
            throw new ElementExistsException("Such element has already been added");
        }

        base.Add(value);
    }

    /// <summary>
    /// Method to insert value at given index. Throws an exception if element is already in list
    /// </summary>
    /// <param name="index">Where to insert value</param>
    /// <param name="value">Value to insert</param>
    /// <exception cref="OutOfRangeException">Exception is thrown if index is less than 0 or
    /// bigger than list.Count and it is impossible to insert value</exception>
    /// <exception cref="ElementExistsException">Thrown if this element already exists in list</exception>
    public override void Insert(int index, int value)
    {
        if (Contains(value))
        {
            throw new ElementExistsException("Such element has already been added");
        }

        base.Insert(index, value);
    }
}
