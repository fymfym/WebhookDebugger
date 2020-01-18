using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class WebhookDebuggerController : ControllerBase
    {
        private readonly ILogger<WebhookDebuggerController> _logger;
        private readonly IEndpointService _endpointService;

        public WebhookDebuggerController(
            ILogger<WebhookDebuggerController> logger,
            IEndpointService endpointService)
        {
            _logger = logger;
            _endpointService = endpointService;
        }

        [HttpGet]
        [Route("{identification}")]
        public IActionResult Get(Guid identification)
        {
            return Handler(identification);
        }

        [HttpOptions]
        [Route("{identification}")]
        public IActionResult Options(Guid identification)
        {
            return Handler(identification);
        }


        [HttpPut]
        [Route("{identification}")]
        public IActionResult Put(Guid identification)
        {
            return Handler(identification);
        }

        [HttpPost]
        [Route("{identification}")]
        public IActionResult Post(Guid identification)
        {
            return Handler(identification);
        }

        [HttpDelete]
        [Route("{identification}")]
        public IActionResult Delete(Guid identification)
        {
            return Handler(identification);
        }

        private IActionResult Handler(Guid identification)
        {
            var endpoint = _endpointService.GetEndpoint(identification);
            if (endpoint == null) return NotFound();
            _endpointService.AddCall(identification, new Call(HttpContext));
            return Ok();
        }
    }
}
