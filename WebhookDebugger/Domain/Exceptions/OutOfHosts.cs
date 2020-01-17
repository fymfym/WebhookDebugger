
namespace WebhookDebugger.Domain.Exceptions
{
    public class OutOfHosts : BaseException
    {
        public OutOfHosts(string message) : base(message)
        {}

        public OutOfHosts()
        { }
    }
}
