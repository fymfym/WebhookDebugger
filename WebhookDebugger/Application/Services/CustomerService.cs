using System;
using System.Collections.Generic;
using System.Linq;
using WebhookDebugger.Domain.Exceptions;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer CreateCustomer(string sender)
        {
            var result = new Customer
            {
                MaxCalls = 4,
                Calls = new List<Call>(),
                CreatedAt = DateTime.Now,
                Identification = Guid.NewGuid(),
                LastCall = DateTime.MinValue,
                SenderAddress = sender
            };

            var customers = _customerRepository.GetCustomerByHost(sender);
            if (customers.Count() >= 3) throw new OutOfHosts();

            return _customerRepository.SaveCustomer(result);
        }

        public Customer GetCustomer(Guid identification)
        {
            var customer = _customerRepository.GetCustomer(identification);
            return customer;
        }

        public bool AddCall(Guid identification, Call call)
        {
            var customer = _customerRepository.GetCustomer(identification);
            if (customer == null) return false;
            customer.Calls.Add(call);
            while (customer.Calls.Count > customer.MaxCalls)
            {
                customer.Calls.RemoveAt(0);
            }

            return true;
        }

        public List<Call> GetCalls(Guid identification)
        {
            var customer = _customerRepository.GetCustomer(identification);
            return customer?.Calls;
        }
    }
}
