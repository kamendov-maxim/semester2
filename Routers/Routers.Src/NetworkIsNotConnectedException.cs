namespace Routers;

public class NetworkIsNotConnectedException: Exception
{
    public NetworkIsNotConnectedException() : base() {}
    public NetworkIsNotConnectedException(string message) : base(message) {}
}
