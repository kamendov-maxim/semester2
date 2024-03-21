using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace LZW.Dependencies;

/// <summary>
/// Класс, содержащий в себе методы для преобразования и обратного
/// преобразования Барроуза-Уилера для байтовых массивов
/// </summary>
public class BWT
{
    /// <summary>
    ///  Строит преобразование Барроуза-Уилера
    /// </summary>
    /// <param name="bytes">Байтовый массив, для которого нужно построить 
    /// преобразование</param>
    /// <returns>Байтовый массив - исходный массив после преобразования\n
    /// и число - позиция исходной строке в таблице циклических сдвигов</returns>
    public static (byte[], int) Transform(byte[] bytes)
    {
        var indexArray = new int[bytes.Length];
        int BWTPosition = 0;

        for (int i = 0; i < bytes.Length; ++i)
        {
            indexArray[i] = i;
        }

        Array.Sort(indexArray, (a, b) => Compare(a, b, bytes));

        var transformedBytes = new byte[bytes.Length];
        for (int i = 0; i < bytes.Length; ++i)
        {
            if (indexArray[i] == 0)
            {
                BWTPosition = i;
                transformedBytes[i] = bytes[bytes.Length - 1];
                continue;
            }
            transformedBytes[i] = bytes[indexArray[i] - 1];
        }

        return (transformedBytes, BWTPosition);
    }


    /// <summary>
    /// Обратное преобразование
    /// </summary>
    /// <param name="bytes">Байтовая последовательность, по 
    /// которой нужно восстановить исходную</param>
    /// <param name="BWTPosition">Позициая исходной строки в 
    /// таблице циклических сдвигов</param>
    /// <returns>Исзходная байтовая последовательность</returns>
    public static byte[] Revert(byte[] bytes, int BWTPosition)
    {
        var sortedBytes = new byte[bytes.Length];
        Array.Copy(bytes, sortedBytes, bytes.Length);
        Array.Sort(sortedBytes);

        var result = new byte[bytes.Length];

        for (int i = 0; i < bytes.Length; ++i)
        {
            byte currentByte = sortedBytes[BWTPosition];
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
                if (bytes[j] == currentByte)
                {
                    ++countL;
                }
            }
            BWTPosition = j - 1;
        }

        return result;
    }

    private static int Compare(int a, int b, byte[] bytes)
    {
        int min = bytes.Length - (a > b ? a : b);
        for (int i = 0; i < min; ++i)
        {
            if (bytes[a + i] < bytes[b + i])
            {
                return -1;
            }
            else if (bytes[a + i] > bytes[b + i])
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
    }

}
