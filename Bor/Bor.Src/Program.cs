using System.Runtime.InteropServices;
using Data_Structures;

class Program
{
    public enum UserInput
    {
        addEntry,
        checkWord,
        removeEntry,
        wordsWithPrefix,
        printSize,
        exitProgram
    }

    public static void PrintOptions()
    {
        Console.WriteLine("Введите команду:");
        Console.WriteLine("0 - добавить слово");
        Console.WriteLine("1 - проверить, есть ли такое слово");
        Console.WriteLine("2 - удалить слово");
        Console.WriteLine("3 - посчитать количество слов с префиксом");
        Console.WriteLine("4 - посчитать размер бора");
        Console.WriteLine("5 - выйти");
    }

    public static UserInput GetUserInput()
    {
        int userInput = Convert.ToInt32(Console.ReadLine());
        while (!typeof(UserInput).IsEnumDefined(userInput))
        {
            Console.WriteLine("Введите цифру от 0 до 5");
            userInput = Convert.ToInt32(Console.ReadLine());
        }
        return (UserInput)userInput;
    }

    public static void Main()
    {
        var myBor = new Bor();
        var input = UserInput.checkWord;
        while (input != UserInput.exitProgram)
        {
            PrintOptions();
            input = GetUserInput();
            switch (input)
            {
                case UserInput.addEntry:
                    {
                        Console.WriteLine("Введите строку");
                        string? str = Console.ReadLine();
                        myBor.Add(str!);
                        Console.WriteLine("Строка добавлена");
                        break;
                    }
                case
                    UserInput.checkWord:
                    {
                        Console.WriteLine("Введите строку");
                        string? str = Console.ReadLine();
                        if (myBor.Contains(str!))
                        {
                            Console.WriteLine("Такая строка есть");
                        }
                        else
                        {
                            Console.WriteLine("Такой строки нет");
                        }
                        break;
                    }
                case UserInput.printSize:
                    {
                        Console.WriteLine($"Размер бора - {myBor.Size}");
                        break;
                    }
                case UserInput.removeEntry:
                    {

                        Console.WriteLine("Введите строку");
                        string? str = Console.ReadLine();
                        myBor.Remove(str!);
                        Console.WriteLine("Строка удалена");
                        break;
                    }
                case UserInput.wordsWithPrefix:
                    {
                        Console.WriteLine("Введите префикс");
                        string? str = Console.ReadLine();
                        Console.WriteLine($"Слов с таким префиксом: {myBor.HowManyStartsWithPrefix(str!)}");
                        break;
                    }
                case UserInput.exitProgram:
                    {
                        Console.WriteLine("Заврешение работы программы");
                        break;
                    }
            }
        }
    }
}
