using System.Collections;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace Burrows_Wheeler;

/// <summary>
/// Implementation of Burrows-Wheeler transform algorithm for strings
/// </summary>
public class BWT
{
    /// <summary>
    /// Transform string
    /// </summary>
    /// <param name="input">Input to encode</param>
    /// <returns>Tuple containing new string and index of original string in a table of cyclic permutation</returns>
    public static (string?, int) Transform(string? input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        var indexArray = new int[input.Length];
        int BWTPosition = 0;

        var charArray = input.ToCharArray();

        for (int i = 0; i < input.Length; ++i)
        {
            indexArray[i] = i;
        }

        Array.Sort(indexArray, (a, b) => Compare(a, b, charArray));

        var transformedArray = new char[charArray.Length];
        for (int i = 0; i < charArray.Length; ++i)
        {
            if (indexArray[i] == 0)
            {
                BWTPosition = i;
                transformedArray[i] = charArray[charArray.Length - 1];
                continue;
            }
            transformedArray[i] = charArray[indexArray[i] - 1];
        }

        return (new string(transformedArray), BWTPosition);
    }

    /// <summary>
    /// Decode encoded string
    /// </summary>
    /// <param name="input">String to decode</param>
    /// <param name="BWTPosition">Position of original string in a table of cyclic permutations that
    /// you got after transforming original string</param>
    /// <returns>Decoded string</returns>
    public static string? Revert(string? input, int BWTPosition)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);

        var charArray = input.ToCharArray();
        var sortedBytes = new char[charArray.Length];
        Array.Copy(charArray, sortedBytes, charArray.Length);
        Array.Sort(sortedBytes);

        var result = new char[charArray.Length];

        for (int i = 0; i < charArray.Length; ++i)
        {
            char currentByte = sortedBytes[BWTPosition];
            result[i] = currentByte;

            int countF = 0;
            for (int k = 0; k < BWTPosition + 1; ++k)
            {
                if (sortedBytes[k] == currentByte)
                {
                    ++countF;
                }
            }

            int countL = 0;
            int j = 0;
            for (; countL != countF; ++j)
            {
                if (charArray[j] == currentByte)
                {
                    ++countL;
                }
            }
            BWTPosition = j - 1;
        }

        return new string(result);
    }

    private static int Compare(int a, int b, char[] input)
    {
        for (int i = 0; i < input.Length; ++i)
        {
            if (a + i >= input.Length)
            {
                a = i * -1;
            }
            if (b + i >= input.Length)
            {
                b = i * -1;
            }
            if (input[a + i] < input[b + i])
            {
                return -1;
            }
            else if (input[a + i] > input[b + i])
            {
                return 1;
            }
        }
        return 0;
    }
}
