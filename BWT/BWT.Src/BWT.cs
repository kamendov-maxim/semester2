using System.Collections;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace Burrows_Wheeler;

public class BWT
{
        public static (string?, int) Transform(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
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


    public static string? Revert(string input, int BWTPosition)
    {
        ArgumentNullException.ThrowIfNull(input);

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
        int min = input.Length - (a > b ? a : b);
        for (int i = 0; i < min; ++i)
        {
            if (input[a + i] < input[b + i])
            {
                return -1;
            }
            else if (input[a + i] > input[b + i])
            {
                return 1;
            }
        }
        if (a > b)
        {
            return -1;
        }
        else if (a < b)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    //     for (int i = 0; i < min; ++i)
    //     {
    //         int comparisonResult = input[a + i].CompareTo(input[b + i]);
    //         if (comparisonResult != 0)
    //         {
    //             return comparisonResult < 0 ? -1 : 1;
    //         }
    //     }
    //     if (a > b)
    //     {
    //         return -1;
    //     }
    //     else if (a < b)
    //     {
    //         return 1;
    //     }
    //     else
    //     {
    //         return 0;
    //     }
    }
}
