namespace Routers;

/// <summary>
/// Exception that is thrown when network is not connected
/// </summary>
public class NetworkIsNotConnectedException: Exception
{
    public NetworkIsNotConnectedException() : base() {}
    public NetworkIsNotConnectedException(string message) : base(message) {}
}
