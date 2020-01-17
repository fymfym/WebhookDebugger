using System;
using System.Collections.Generic;
using WebhookDebugger.Domain.Models;

namespace WebhookDebugger.Domain.Services
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(Guid identification);
        IEnumerable<Customer> GetCustomerByHost(string sender);
        Customer SaveCustomer(Customer customer);
    }
}
