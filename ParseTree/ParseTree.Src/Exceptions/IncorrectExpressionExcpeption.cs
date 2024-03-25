namespace ParseTree;

public class IncorrectExpressionException: Exception
{
    public IncorrectExpressionException() : base() { }
    public IncorrectExpressionException(string message) : base(message) { }
}
