using System.Linq.Expressions;
using ParseTree;

string options = """
                    0 - Exit
                    1 - Create tree
                    2 - Evaluate tree
                    3 - Save tree to file
                    4 - Print expression
                    Write number to console to choose option:
                    """;
bool running = true;
Tree? currentTree = null;
while (running)
{
    Console.WriteLine(options);
    int userInput;
    while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 4 || userInput < 0)
    {
        Console.WriteLine("Incorrect input");
        Console.WriteLine(options);
    }

    switch (userInput)
    {
        case 0:
            {
                Console.Write("Exit");
                running = false;
                break;
            }
        case 1:
            {
                Console.WriteLine("Write your ariphmetical expression in postfix form to console");
                string? expression = Console.ReadLine();
                try
                {
                    if (expression is not null)
                    {
                        currentTree = new(expression);
                    }
                }
                catch (IncorrectExpressionException)
                {
                    Console.WriteLine("Incorrect input\nYour expression must fit the template (<operation> <operand1> <operand2>) where operands may be expressions themselves");
                }
                break;
            }
        case 2:
            {
                try
                {
                    if (currentTree is not null)
                    {
                        var answer = currentTree.Evaluate();
                        Console.WriteLine($"Answer to your expression is {answer}");
                    }
                    else
                    {
                        Console.WriteLine("You haven't added an expression yet");
                    }
                }
                catch (IncorrectExpressionException)
                {
                    Console.WriteLine("Seems like your expression was incorrect. Please write another one");
                }
                catch (IncorrectTreeException)
                {
                    Console.WriteLine("Seems like there is a problem with your tree");
                }
                break;
            }
        case 3:
            {
                Console.WriteLine("Write name for the file where you want to save your expression");
                if (currentTree is not null)
                {
                    string? filename = Console.ReadLine();
                    if (filename is not null && filename != string.Empty)
                    {
                        if (filename[0] != '/' || filename[0] != '~' || filename[0] != '.')
                        {
                            filename = "./" + filename;
                        }
                        try
                        {
                            string? expression = currentTree.ToString();
                            File.WriteAllText(filename, expression);
                            Console.Write("Your expression was saved at ");
                            Console.WriteLine(filename);
                        }
                        catch (IncorrectTreeException)
                        {
                            Console.WriteLine("Seems like there is a problem with your tree");
                        }

                    }
                    else
                    {
                        Console.WriteLine("You should write a filename for your file");
                    }
                }
                else
                {
                    Console.WriteLine("You haven't added an expression yet");
                }
                break;
            }
        case 4:
            {
                if (currentTree is not null)
                {
                    try
                    {
                        string? expression = currentTree.ToString();
                        Console.Write("Your expression: ");
                        Console.WriteLine(expression);
                    }
                    catch (IncorrectTreeException)
                    {
                        Console.WriteLine("Seems like there is a problem with your tree");
                    }
                }
                else
                {
                    Console.WriteLine("You haven't added an expression yet");
                }
                break;
            }
    }
    Console.WriteLine();
}
