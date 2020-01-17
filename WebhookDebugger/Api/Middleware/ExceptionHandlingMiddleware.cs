using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebhookDebugger.Domain.Exceptions;

namespace WebhookDebugger.Api.Middleware
{
    // CA2007: Do not directly await a Task
    [SuppressMessage("ReSharper", "CA2007")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class ExceptionLogging
    {
        private readonly ILogger<ExceptionLogging> _logger;
        private readonly RequestDelegate _requestDelegate;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public ExceptionLogging(ILogger<ExceptionLogging> logger, RequestDelegate requestDelegate, ICorrelationContextAccessor correlationContextAccessor)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
            _correlationContextAccessor = correlationContextAccessor;
        }

        [SuppressMessage("ReSharper", "CA1031")]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (BaseException e)
            {
                var errorObject = new ErrorMessageObject(400, e.GetType().ToString());
                httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorObject));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{@correlationId}", _correlationContextAccessor?.CorrelationContext?.CorrelationId);
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                await httpContext.Response.WriteAsync(e.Message);
            }
        }
    }
}
