namespace Lists;

public class ElementExistsException: Exception
{
    public ElementExistsException() { }
    public ElementExistsException(string message) : base(message) { }

    public ElementExistsException(string message, Exception innerException) : base(message, innerException) { }
}
