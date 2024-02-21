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

    static void Main()
    {
        string? input = Console.ReadLine();
        (string str, int index) = Transform(input);
        Console.WriteLine(str);
        Console.WriteLine(index);
    }
}
