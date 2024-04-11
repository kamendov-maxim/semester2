using System.Diagnostics.Contracts;
using Compressor;

if (args.Length != 2 || args[0] == "-h" || args[0] == "--help")
{
    PrintHelp();
}
else
{
    if (args[0] == "-c")
    {
        Console.WriteLine("Производится сжатие файла...");
        try
        {
            double coefficient = Compressor.LZW.Compress(args[1], true);
            Console.WriteLine($"Сжатый файл находится по пути {args[1] + ".zipped"}");
            Console.WriteLine($"Коэффициент сжатия {coefficient}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл по указанному пути не найден");
        }
        catch (InvalidDataException)
        {
            Console.WriteLine("Ошибка во время чтения файла");
        }
    }
    else if (args[0] == "-u")
    {
        Console.WriteLine("Производится разжатие файла...");
        try
        {
            Compressor.LZW.Decompress(args[1]);
            Console.WriteLine($"Разжатый файл находится по пути {(args[1]).Substring(0, args[1].Length - 7)}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл по указанному пути не найден");
        }
        catch (InvalidDataException)
        {
            Console.WriteLine("Ошибка во время чтения файла");
        }
    }
    else
    {
        PrintHelp();
    }
}

static void PrintHelp()
{
    Console.WriteLine("LZW [-h | --help] - показать это сообщение\n");
    Console.WriteLine("LZW -c <path-to-file> - сжать файл, находящийся по заданному пути");
    Console.WriteLine("Размещает сжатый файл в той же директории, с тем же названием и расширением .zipped\n");
    Console.WriteLine("LZW -u <path-to-file> - разжать файл, ранее сжатый с помощью LZW");
    Console.WriteLine("Размещает исходный файл в той же директории и с тем же названием, что сжатый, но без расширения .zipped");
}
