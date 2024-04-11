namespace Vector;

public class SparseVector
{
    private readonly SortedDictionary<int, int> dictionary;
    public readonly int Length;
    public SparseVector(int[] vector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        dictionary = [];
        Length = vector.Length;

        int i = 0;
        foreach (var number in vector)
        {
            if (number != 0)
            {
                dictionary[i] = number;
            }
            ++i;
        }
    }

    public bool IsZeroVector()
    {
        return dictionary.Count == 0;
    }

    public int[] ToArray()
    {
        var array = new int[Length];
        for (int i = 0; i < Length; ++i)
        {
            dictionary.TryGetValue(i, out int currentNumber);
            array[i] = currentNumber;
        }
        return array;
    }

    public int ScalarMultiplicationWith(SparseVector secondVector)
    {
        if (Length != secondVector.Length)
        {
            throw new DifferentLengthVectorsException("You can't do any operations with vectors of different length");
        }
        if (dictionary.Count == 0 || secondVector.IsZeroVector())
        {
            return 0;
        }
        int result = 0;
        for (int i = 0; i < Length; ++i)
        {
            dictionary.TryGetValue(i, out int firstNumber);
            result += secondVector.GetNumberAtIndex(i) * firstNumber;
        }
        return result;
    }

    public void Substract(SparseVector secondVector)
    {
        if (Length != secondVector.Length)
        {
            throw new DifferentLengthVectorsException("You can't do any operations with vectors of different length");
        }
        if (!secondVector.IsZeroVector())
        {
            SumAndSubstract(secondVector, true);
        }
    }

    public void Sum(SparseVector secondVector)
    {
        if (Length != secondVector.Length)
        {
            throw new DifferentLengthVectorsException("You can't do any operations with vectors of different length");
        }
        if (!secondVector.IsZeroVector())
        {
            SumAndSubstract(secondVector, false);
        }
    }

    private void SumAndSubstract(SparseVector secondVector, bool substract)
    {
        int multiplier = substract ? -1 : 1;

        for (int i = 0; i < Length; ++i)
        {
            int newValueAtIndex = 0;
            dictionary.TryGetValue(i, out int currentElement);
            newValueAtIndex += currentElement;
            newValueAtIndex += secondVector.GetNumberAtIndex(i) * multiplier;
            if (newValueAtIndex == 0)
            {
                dictionary.Remove(i);
            }
            else
            {
                dictionary[i] = newValueAtIndex;
            }
        }
    }

    public int GetNumberAtIndex(int index)
    {
        dictionary.TryGetValue(index, out int result);
        return result;
    }
}
