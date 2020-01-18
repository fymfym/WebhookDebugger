using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebhookDebugger.Domain.Exceptions;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Application.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;

        public EndpointService(
            IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<Endpoint> CreateEndpoint(string sender)
        {
            await _endpointRepository.CleanupOldestEndpoints();

            var result = new Endpoint
            {
                MaxCalls = 4,
                Calls = new List<Call>(),
                CreatedAt = DateTime.Now,
                Identification = Guid.NewGuid(),
                LastCall = DateTime.MinValue,
                SenderAddress = sender
            };

            var endpoints = await _endpointRepository.GetEndpointByHost(sender);
            if (endpoints.Count() >= 10) throw new OutOfEndpointsPrHosts();

            return await _endpointRepository.SaveEndpoint(result);
        }

        public async Task<Endpoint> GetEndpoint(Guid identification)
        {
            var endpoint = await _endpointRepository.GetEndpoint(identification);
            return endpoint;
        }

        public async Task<bool> AddCall(Guid identification, Call call)
        {
            var endpoint = await _endpointRepository.GetEndpoint(identification);
            if (endpoint == null) return false;
            endpoint.Calls.Add(call);
            while (endpoint.Calls.Count > endpoint.MaxCalls)
            {
                endpoint.Calls.RemoveAt(0);
            }

            return true;
        }

        public async Task<List<Call>> GetCalls(Guid identification)
        {
            var endpoint = await _endpointRepository.GetEndpoint(identification);
            return endpoint?.Calls;
        }
    }
}
