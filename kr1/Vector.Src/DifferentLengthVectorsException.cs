// copyright file: https://github.com/kamendov-maxim/semester2/blob/master/LICENSE
public class DifferentLengthVectorsException : Exception
{
    public DifferentLengthVectorsException() { }
    public DifferentLengthVectorsException(string message) : base(message) { }
    public DifferentLengthVectorsException(string message, Exception inner) : base(message, inner) { }
}
