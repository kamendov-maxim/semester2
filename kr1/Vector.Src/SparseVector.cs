// copyright file: https://github.com/kamendov-maxim/semester2/blob/master/LICENSE
namespace Vector;

/// <summary>
/// Implementation of SparseVector
/// </summary>
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

    /// <summary>
    /// Check if vector contains only zeros
    /// </summary>
    /// <returns>True if there are no other numbers than zeros</returns>
    public bool IsZeroVector()
    {
        return dictionary.Count == 0;
    }

    /// <summary>
    /// Convert SparseVector to int array
    /// </summary>
    /// <returns>Uncompressed array containing all the numbers from vector</returns>
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

    /// <summary>
    /// Scalar multiplication on two vectors
    /// </summary>
    /// <param name="secondVector">Vector you want to multiply your vector with</param>
    /// <returns>Result of scalat multiplication</returns>
    /// <exception cref="DifferentLengthVectorsException">Thrown if lengths of vectors are different</exception>
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

    /// <summary>
    /// Substract one vector from another
    /// </summary>
    /// <param name="secondVector">Vector you want to substract from your vector</param>
    /// <exception cref="DifferentLengthVectorsException">Thrown if lengths of vectors are different</exception>
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

    /// <summary>
    /// Sum one vector from another
    /// </summary>
    /// <param name="secondVector">Vector you want to sum with your vector</param>
    /// <exception cref="DifferentLengthVectorsException">Thrown if lengths of vectors are different</exception>
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

    /// <summary>
    /// GetNumber located at the given index in vector
    /// </summary>
    /// <param name="index">index</param>
    /// <returns>Number at index</returns>
    public int GetNumberAtIndex(int index)
    {
        dictionary.TryGetValue(index, out int result);
        return result;
    }
}
