class BWT
{
    public static (string, int) Transform(string input)
    {
        int[] suffixArray = new int[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            suffixArray[i] = i;
        }

        Array.Sort(suffixArray, (a, b) => String.Compare(input[a..], input[b..]));

        char[] charArray = new char[input.Length];
        int indexOfDefaultString = 0;
        for (int i = 0; i < input.Length; ++i)
        {
            int currentIndex = suffixArray[i];
            if (currentIndex == 0)
            {
                indexOfDefaultString = i;
            }
            charArray[i] = input[currentIndex == 0 ? input.Length - 1 : currentIndex - 1];
        }

        return (new string(charArray), indexOfDefaultString);
    }

    public static string Revert(string input, int index)
    {
        char[] charArray = new char[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            charArray[i] = input[i];
        }
        Array.Sort(charArray);

        char[] wordArray = new char[input.Length];

        for (int i = 0; i < input.Length; ++i)
        {
            char currentCharacter = charArray[index];
            wordArray[i] = currentCharacter;

            int countF = 0;
            for (int k = 0; k < index + 1; k++)
            {
                if (charArray[k] == currentCharacter)
                {
                    ++countF;
                }
            }

            int countL = 0;
            int j = 0;
            for (; countL != countF; j++)
            {
                if (input[j] == currentCharacter)
                {
                    ++countL;
                }
            }
            index = j - 1;
        }

        return new string(wordArray);
    }

    static void Main()
    {
        Console.WriteLine("Введите строку");        
        string? input = Console.ReadLine();
        (string str, int index) = Transform(input);
        Console.WriteLine($"Строка после преобразования: {str}\nИндекс строчки с исходной строкой в таблице: {index}");

        string st1 = Revert(str, index);
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
