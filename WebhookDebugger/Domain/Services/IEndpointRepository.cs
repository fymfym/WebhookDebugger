using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebhookDebugger.Domain.Models;

namespace WebhookDebugger.Domain.Services
{
    public interface IEndpointRepository
    {
        Task<Endpoint> GetEndpoint(Guid identification);
        Task<IEnumerable<Endpoint>> GetEndpointByHost(string sender);
        Task CleanupOldestEndpoints();
        Task<Endpoint> SaveEndpoint(Endpoint endpoint);
    }
}
