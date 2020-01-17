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
        private readonly ICustomerService _customerService;

        public AdminController(
            ILogger<WebhookDebuggerController> logger,
            ICustomerService customerService
            )
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public Task<UserDto> Post()
        {
            var sender = HttpContext.Request.Host.Host;
            var customer = _customerService.CreateCustomer(sender);

            var result = new UserDto
            {
                Identification = customer.Identification
            };

            return Task.FromResult(result);
        }

        [HttpGet]
        [Route("{identification}")]
        public Task<List<Call>> Get(Guid identification)
        {
            var customer = _customerService.GetCustomer(identification);
            if (customer == null) return null;
            var result = customer.Calls;
            result.Reverse();
            return Task.FromResult(result);
        }
    }
}
