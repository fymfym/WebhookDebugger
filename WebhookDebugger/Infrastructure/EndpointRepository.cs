using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebhookDebugger.Domain.Models;
using WebhookDebugger.Domain.Services;

namespace WebhookDebugger.Infrastructure
{
    public class EndpointRepository : IEndpointRepository
    {
        private readonly List<Endpoint> _endpointList;

        public EndpointRepository()
        {
            _endpointList = new List<Endpoint>();
        }

        public async Task<Endpoint> GetEndpoint(Guid identification)
        {
            var returnValue = _endpointList.All(x => x.Identification != identification) ? null : _endpointList.Single(x => x.Identification == identification);
            return await Task.FromResult(returnValue);
        }

        public async Task CleanupOldestEndpoints()
        {
            if (_endpointList.Count < 100) return;
            var item = _endpointList.OrderBy(x => x.CreatedAt).First();
            _endpointList.Remove(item);
            await Task.Run(() => { });
        }

        public async Task<IEnumerable<Endpoint>> GetEndpointByHost(string sender)
        {
            var returnResult = _endpointList.Where(x => x.SenderAddress == sender);
            return await Task.FromResult(returnResult);
        }

        public async Task<Endpoint> SaveEndpoint(Endpoint endpoint)
        {
            _endpointList.Add(endpoint);
            return await Task.FromResult(endpoint);
        }
    }
}
