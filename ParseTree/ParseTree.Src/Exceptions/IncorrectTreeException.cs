namespace ParseTree;

public class IncorrectTreeException: Exception
{
    public IncorrectTreeException() : base() { }
    public IncorrectTreeException(string message) : base(message) { }
}
