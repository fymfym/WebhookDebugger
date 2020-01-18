
namespace WebhookDebugger.Domain.Exceptions
{
    public class OutOfEndpointsPrHosts : BaseException
    {
        public OutOfEndpointsPrHosts(string message) : base(message)
        {}

        public OutOfEndpointsPrHosts()
        { }
    }
}
