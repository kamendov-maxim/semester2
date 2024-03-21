using System.Globalization;
using System.IO.Compression;
using LZW.Dependencies;

namespace Compressor;

/// <summary>
/// Класс, содержащий методы, позволяющие сжать или разжать файл с помощью алгоритма Зива-Лемпеля-Уэлча
/// </summary>
public class LZW
{
    /// <summary>
    /// Метод, производящий сжатие файла
    /// </summary>
    /// <param name="path">Путь до файла</param>
    /// <param name="usingBwt">Сообщает методу, нужно ли использовать преобразование Бароуза-Уилера</param>
    /// <returns>Коэффициент сжатия файла - во сколько раз сжатый файл занимает памяти меньше, чем исходный</returns>
    /// <exception cref="FileNotFoundException">Возникает в случае, если файл по указанному пути не найден</exception>
    /// <exception cref="InvalidDataException">Возникает в случае, если файл по указанному пути пустой</exception>
    public static double Compress(string path, bool usingBwt = true)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Файл не найден");
        }

        var bytes = File.ReadAllBytes(path);

        if (bytes.Length == 0)
        {
            throw new InvalidDataException("Файл пустой");
        }

        int bwtPosition = -1;
        if (usingBwt)
        {
            (bytes, bwtPosition) = BWT.Transform(bytes);
        }

        var compressedBytes = Compressor.Compress(bytes, bwtPosition);

        File.WriteAllBytes(path + ".zipped", compressedBytes);
        return (double)(bytes.Length / compressedBytes.Length);
    }

    /// <summary>
    /// Метод, производящий разжатие файла
    /// </summary>
    /// <param name="path">Путь до файла</param>
    /// <exception cref="FileNotFoundException">Возникает в случае, если файл по указанному пути не найден</exception>
    /// <exception cref="InvalidDataException">Возникает в случае, если файл по указанному пути пустой</exception>
    public static void Decompress(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Файл не найден");
        }

        var bytes = File.ReadAllBytes(path);

        if (bytes.Length == 0)
        {
            throw new InvalidDataException("Файл пустой");
        }

        var decompressedBytes = Decompressor.Decompress(bytes);

        File.WriteAllBytes(path.Substring(0, path.Length - 7), decompressedBytes);
    }
}
