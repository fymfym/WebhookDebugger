using System;
using System.Collections.Generic;
using WebhookDebugger.Domain.Models;

namespace WebhookDebugger.Domain.Services
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string sender);

        Customer GetCustomer(Guid identification);

        bool AddCall(Guid identification, Call call);

        List<Call> GetCalls(Guid identification);
    }
}
