using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using ParseTree.Dependencies;

namespace ParseTree;

/// <summary>
/// Data structure for storing ariphmetical expressions
/// </summary>
public class Tree
{
    private INode root;

    public Tree(string expression)
    {
        if (expression is null || expression == string.Empty)
        {
            throw new IncorrectExpressionException("Empty expression");
        }

        int index = 0;
        SkipSpaces(expression, ref index);
        if (expression[index] >= '0' && expression[index] <= '9')
        {
            int number = ParseNumber(expression, ref index);
            if (index != expression.Length)
            {
                throw new IncorrectExpressionException();
            }
            root = new NumberNode(number);
        }
        else
        {
            root = Parse(expression, ref index);
        }
    }

    private static INode Parse(string expression, ref int index)
    {
        INode newNode;
        SkipSpaces(expression, ref index);
        char currentChar = expression[index];
        if (currentChar == '(')
        {
            ++index;
            newNode = new OperationNode(expression[index]);
            ++index;
            newNode.LeftChild = Parse(expression, ref index);
            newNode.RightChild = Parse(expression, ref index);
            SkipSpaces(expression, ref index);
            if (expression[index] == ')')
            {
                ++index;
            }
            else
            {
                throw new IncorrectExpressionException();
            }
        }
        else if (currentChar >= '0' && currentChar <= '9')
        {
            int number = ParseNumber(expression, ref index);
            newNode = new NumberNode(number);
        }
        else
        {
            throw new IncorrectExpressionException();
        }

        return newNode;
    }

    private static void SkipSpaces(string expression, ref int index)
    {
        while (expression[index] == ' ')
        {
            ++index;
        }
    }

    private static int ParseNumber(string expression, ref int index)
    {
        int currentNumber = 0;
        char currentChar = expression[index];
        while (currentChar >= '0' && currentChar <= '9')
        {
            currentNumber *= 10;
            currentNumber += expression[index] - '0';
            ++index;
            if (currentChar >= expression.Length)
            {
                break;
            }
            currentChar = expression[index];
        }
        return currentNumber;
    }

    /// <summary>
    /// Evaluate the expression stored in the tree
    /// </summary>
    /// <returns>Answer to expression</returns>
    public double Evaluate()
    {
        return root.Eval();
    }

    /// <summary>
    /// Write the expression in postfix form to string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return root.ToString();
    }
}
