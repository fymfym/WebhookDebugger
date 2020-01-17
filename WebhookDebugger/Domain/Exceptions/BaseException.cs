using System;

namespace WebhookDebugger.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {}

        public BaseException()
        {}
    }
}
