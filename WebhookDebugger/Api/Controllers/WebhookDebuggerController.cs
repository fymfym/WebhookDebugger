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
        private readonly ICustomerService _customerService;

        public WebhookDebuggerController(
            ILogger<WebhookDebuggerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
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
            var customer = _customerService.GetCustomer(identification);
            if (customer == null) return NotFound();
            _customerService.AddCall(identification, new Call(HttpContext));
            return Ok();
        }
    }
}
