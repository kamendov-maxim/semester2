using System.ComponentModel.DataAnnotations;
using CalculatorNamespace;
using Stack;

class Program
{

    public static void Main()
    {
        Console.Write("Введите выражение: ");
        string? expression = Console.ReadLine();
        var listStack = new ListStack();
        var stackWithNodes = new StackWithNodes();
        (double answer1, Calculator.ErrorCode ec1) = Calculator.Evaluate(expression, listStack);
        (double answer2, Calculator.ErrorCode ec2) = Calculator.Evaluate(expression, stackWithNodes);
        if (ec1 == Calculator.ErrorCode.Ok && ec2 == Calculator.ErrorCode.Ok)
        {
            Console.WriteLine($"Результат работы при использовании стека на списках: {answer1}\nБез списков: {answer2}");
        }
        else if (ec1 == Calculator.ErrorCode.DivisionByZero || ec2 == Calculator.ErrorCode.DivisionByZero)
        {
            Console.WriteLine("Вы не можете делить на ноль");
        }
        else if (ec1 == Calculator.ErrorCode.WrongOperator || ec2 == Calculator.ErrorCode.WrongOperator)
        {
            Console.WriteLine("Проверьте правильность ввода");
        }
        else if (ec1 == Calculator.ErrorCode.ExpressionIsNull || ec2 == Calculator.ErrorCode.ExpressionIsNull)
        {
            Console.WriteLine("Выражение не может быть пустой строкой");
        }
    }
}
