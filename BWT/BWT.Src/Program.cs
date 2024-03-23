using Burrows_Wheeler;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите строку");
        string? input = Console.ReadLine();
        (string? str, int index) = BWT.Transform(input);
        Console.WriteLine($"Строка после преобразования: {str}\nИндекс строчки с исходной строкой в таблице: {index}");

        string? st1 = BWT.Revert(str, index);
        Console.WriteLine($"Строка после обратного преобразования: {st1}");

        if (String.Equals(input, st1))
        {
            Console.WriteLine("Исходная и полученная строки одинаковы");
        }
        else
        {
            Console.WriteLine("Исходная и полученная строки различаются");
        }
    }
}
