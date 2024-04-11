using System.Transactions;

namespace LZW.Dependencies;

/// <summary>
/// Структура данных, хранящая последовательность чисел, из которой можно получить массив
/// </summary>
class NumberBuffer
{
    public int currentBitCount = 8;
    private int currentNumber = 0;
    public int currentNumberLength = 0;
    private const int BITS_IN_BYTE = 8;
    public int MaxNumber = 256;
    public List<int> Numbers;

    public NumberBuffer()
    {
        Numbers = new List<int>();
    }

    /// <summary>
    /// Метод, позволяющий добавить число, зашифрованное байтом (число добавляется, когда наберется нужно число байт для шифрования числа)
    /// </summary>
    /// <param name="oneByte">Байт, который нужно добавить</param>
    /// <returns>True, если текущий байт был последним байтом числа и оно добавилось</returns>
    public bool AddByte(byte oneByte)
    {
        bool newNumber = false;
        var bits = ByteToByteArray(oneByte);
        foreach (var bit in bits)
        {
            currentNumber = (currentNumber * 2) + bit;
            if (++currentNumberLength == currentBitCount)
            {
                AddNumberToBuffer();
                newNumber = true;
            }
        }

        return newNumber;
    }

    /// <summary>
    /// Позволяет получить массив уже добавленных чисел
    /// </summary>
    /// <returns>Массив чисел</returns>
    public int[] ToIntArray()
    {
        AddNumberToBuffer();
        return Numbers.ToArray();
    }

    private void AddNumberToBuffer()
    {
        Numbers.Add(currentNumber);
        currentNumber = 0;
        currentNumberLength = 0;
    }

    private byte[] ByteToByteArray(byte oneByte)
    {
        var bits = new byte[BITS_IN_BYTE];
        for (int i = BITS_IN_BYTE - 1; i >=0; --i)
        {
            bits[i] = (byte)(oneByte % 2);
            oneByte >>= 1;
        }
        
        return bits;
    }
}
