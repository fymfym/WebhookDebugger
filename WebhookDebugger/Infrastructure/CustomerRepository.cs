using System;
using System.Collections.Generic;
using System.Linq;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customerList;

        public CustomerRepository()
        {
            _customerList = new List<Customer>();
        }

        public Customer GetCustomer(Guid identification)
        {
            return _customerList.All(x => x.Identification != identification) ? null : _customerList.Single(x => x.Identification == identification);
        }

        public IEnumerable<Customer> GetCustomerByHost(string sender)
        {
            return _customerList.Where(x => x.SenderAddress == sender);
        }

        public Customer SaveCustomer(Customer customer)
        {
            _customerList.Add(customer);
            return customer;
        }
    }
}
