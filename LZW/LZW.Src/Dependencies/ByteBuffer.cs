namespace LZW.Dependencies;

/// <summary>
/// Структура данных, представляющая собой последовательность чисел в двоичной форме, из которой можно получить байтовый массив
/// </summary>
class ByteBuffer
{
    public int MaxSize = 256;
    public int currentBitCount = 8;
    private const int BITS_IN_BYTE = 8;
    int currentByteSize = 0;
    byte currentByte = 0;
    List<byte> buffer = new();

    /// <summary>
    /// Добавляет 4 байта в начале - позиция исходной строки в таблице циклических сдвигов преобразования Барроуза-Уилера
    /// </summary>
    /// <param name="position">Позиция исходной строки в таблице</param>
    public void SetBWTPosition(int position)
    {
        var positionBytes = BitConverter.GetBytes(position);
        buffer.AddRange(positionBytes);
    }

    /// <summary>
    /// Метод, добавляющий число в буффер
    /// </summary>
    /// <param name="number">Число, которое нужно добавить</param>
    public void AddNumber(int number)
    {
        var bits = IntToByte(number);
        foreach (var bit in bits)
        {
            currentByte = (byte)((currentByte << 1) + bit);
            ++currentByteSize;
            if (currentByteSize == BITS_IN_BYTE)
            {
                currentByteSize = 0;
                buffer.Add(currentByte);
                currentByte = 0;
            }
        }
    }

    /// <summary>
    /// Метод, позволяющий получить байтовый массив из текущего буффера
    /// </summary>
    /// <returns>байтовый массив</returns>
    public byte[] ToByteArray()
    {
        currentByte <<= BITS_IN_BYTE - currentByteSize;
        currentByteSize = 0;
        currentByte = 0;
        buffer.Add(currentByte);
        return buffer.ToArray();
    }

    private byte[] IntToByte(int number)
    {
        var bits = new byte[currentBitCount];
        for (int i = currentBitCount - 1; i >= 0; --i)
        {
            bits[i] = (byte)(number % 2);
            number /= 2;
        }

        return bits;
    }
}
