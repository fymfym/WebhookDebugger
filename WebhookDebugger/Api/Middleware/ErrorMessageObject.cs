using System;
using System.Net;
using Newtonsoft.Json;

namespace WebhookDebugger.Api.Middleware
{
    public class ErrorMessageObject
    {
        public HttpStatusCode StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ErrorMessageObject(HttpStatusCode statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? StatusCode.ToString();
        }

        public ErrorMessageObject(int statusCode, string message = null)
        {
            StatusCode = (HttpStatusCode) statusCode;
            Message = message ?? StatusCode.ToString();
        }

        public ErrorMessageObject(int statusCode, Exception exception)
        {
            StatusCode = (HttpStatusCode) statusCode;
            Message = exception?.ToString() ?? StatusCode.ToString();
        }
    }
}
