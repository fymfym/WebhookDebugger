using System;
using System.Collections.Generic;

namespace WebhookDebugger.Domain.Models
{
    public class Customer
    {
        public Guid Identification { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastCall { get; set; }
        public int MaxCalls { get; set; }

        public string SenderAddress { get; set; }
        public List<Call> Calls { get; set; }
    }
}
