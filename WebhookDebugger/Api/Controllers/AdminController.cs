using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebhookDebugger.Api.Models;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<WebhookDebuggerController> _logger;
        private readonly IEndpointService _endpointService;

        public AdminController(
            ILogger<WebhookDebuggerController> logger,
            IEndpointService endpointService
            )
        {
            _logger = logger;
            _endpointService = endpointService;
        }

        [HttpPost]
        public async Task<UserDto> Post()
        {
            var sender = HttpContext.Request.Host.Host;
            var endpoint = await _endpointService.CreateEndpoint(sender);

            var result = new UserDto
            {
                Identification = endpoint.Identification
            };

            return result;
        }

        [HttpGet]
        [Route("{identification}")]
        public async Task<List<Call>> Get(Guid identification)
        {
            var calls = await _endpointService.GetCalls(identification);
            return calls;
        }
    }
}
