using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using LZW.Dependencies;

namespace Compressor;

internal class Decompressor
{
    public static byte[] Decompress(byte[] bytes)
    {
        if (bytes.Length == 0)
        {
            throw new InvalidDataException("empty byte sequence");
        }

        Dictionary<int, List<byte>> dictionary = new();

        for (int i = 0; i < 256; ++i)
        {
            dictionary[i] = new List<byte> { (byte)(i) };
        }

        int bwtPosition = BitConverter.ToInt32(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] });
        var list = new List<byte>(bytes);
        list.RemoveRange(0, 4);
        bytes = list.ToArray();

        if (bwtPosition != -1)
        {
            bytes = BWT.Revert(bytes, bwtPosition);
        }

        int counter = 256;
        NumberBuffer numberBuffer = new();
        
        for (int i = 0; i < bytes.Length; ++i)
        {
            if (counter == numberBuffer.MaxNumber)
            {
                numberBuffer.MaxNumber *= 2;
                ++numberBuffer.currentBitCount;
            }

            if (numberBuffer.AddByte(bytes[i]))
            {
                ++counter;
            }
        }

        int[] intArray = numberBuffer.ToIntArray();
        counter = 256;

        List<byte> decodedBytes = new();
        for (int i = 0; i < intArray.Length - 1; ++i)
        {
            decodedBytes.AddRange(dictionary[intArray[i]]);

            List<byte> newCodeSequence = new();
            newCodeSequence.AddRange(dictionary[intArray[i]]);

            if (!dictionary.ContainsKey(intArray[i + 1]))
            {
                newCodeSequence.Add(newCodeSequence[0]);
                dictionary[counter] = newCodeSequence;
            }
            else
            {
                newCodeSequence.Add(dictionary[intArray[i + 1]][0]);
                dictionary[counter] = newCodeSequence;
            }

            ++counter;
        }
        return decodedBytes.ToArray();
    }

    // private static (int[], int) getNumbersAndBwtPosition(byte[] bytes)
    // {
    //     int bwtPosition = BitConverter.ToInt32(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] });
    //     var buffer = new NumberBuffer();
    //     for (int i = 5; i < bytes.Length; ++i)
    //     {
    //         var oneByte = bytes[i];
    //         buffer.AddByte(oneByte, true);
    //     }
    //     return (buffer.Numbers.ToArray(), bwtPosition);
    // }
}
