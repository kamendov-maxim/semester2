using Lists;



class Program
{
    public static void Main()
    {
        string options = """
                    What kind of list do you want to create? Write number to console to choose
                    0 - List
                    1 - UniqueList
                    """;

        Console.WriteLine(options);
        int listType;
        while (!int.TryParse(Console.ReadLine(), out listType) || listType > 1 || listType < 0)
        {
            Console.WriteLine("Incorrect input");
            Console.Write(options);
        }
        Console.WriteLine();

        IList list;
        if (listType == 0)
        {
            list = new List();
        }
        else
        {
            list = new UniqueList();
        }

        bool running = true;
        while (running)
        {
            options = """
                        0 - Exit
                        1 - Add element to list
                        2 - Insert element to list
                        3 - Remove element from list
                        4 - Print list
                        Write number to console to choose option
                        """;

            Console.WriteLine(options);

            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || option > 4 || option < 0)
            {
                Console.WriteLine("Incorrect input");
                Console.WriteLine(options);
            }

            switch (option)
            {
                case 0:
                    {
                        running = false;
                        break;
                    }
                case 1:
                    {
                        var value = UserInputTryParse("Write value you want to add");
                        try
                        {
                            list.Add(value);
                        }
                        catch (ElementExistsException)
                        {
                            Console.WriteLine("You have already added this element earlier");
                        }
                        break;
                    }
                case 2:
                    {
                        var index = UserInputTryParse("Write an index where you want to insert an element");
                        var value = UserInputTryParse("Write value you want to insert");

                        try
                        {
                            list.Insert(index, value);
                        }
                        catch (OutOfRangeException)
                        {
                            Console.WriteLine("Index is out of range");
                        }
                        catch (ElementExistsException)
                        {
                            Console.WriteLine("You have already added this element earlier");
                        }
                        break;
                    }
                case 3:
                    {
                        var index = UserInputTryParse("Write an index where you want to delete an element");
                        try
                        {
                            list.RemoveAt(index);
                        }
                        catch (OutOfRangeException)
                        {
                            Console.WriteLine("Index is out of range");
                        }
                        break;
                    }
                case 4:
                    {
                        Console.Write("[");
                        for (int i = 0; i < list.Count; ++i)
                        {
                            Console.Write(list.FindValue(i));
                            if (i != list.Count - 1)
                            {
                                Console.Write(", ");
                            }
                        }
                        Console.Write("]\n");
                        break;
                    }
            }
            Console.WriteLine();
        }
    }

    private static int UserInputTryParse(string message)
    {
        Console.WriteLine(message);
        int userInput;
        while (!int.TryParse(Console.ReadLine(), out userInput))
        {
            Console.WriteLine("Incorrect input");
            Console.WriteLine(message);
        }

        return userInput;
    }
}
