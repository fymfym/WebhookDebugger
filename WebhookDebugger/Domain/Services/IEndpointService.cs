using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebhookDebugger.Domain.Models;

namespace WebhookDebugger.Domain.Services
{
    public interface IEndpointService
    {
        Task<Endpoint> CreateEndpoint(string sender);

        Task<Endpoint> GetEndpoint(Guid identification);

        Task<bool> AddCall(Guid identification, Call call);

        Task<List<Call>> GetCalls(Guid identification);
    }
}
