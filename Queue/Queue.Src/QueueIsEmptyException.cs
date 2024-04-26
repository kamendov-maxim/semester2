namespace DataStructures;

[System.Serializable]
public class QueueIsEmtpyException : System.Exception
{
    public QueueIsEmtpyException() { }
    public QueueIsEmtpyException(string message) : base(message) { }
    public QueueIsEmtpyException(string message, System.Exception inner) : base(message, inner) { }
    protected QueueIsEmtpyException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

