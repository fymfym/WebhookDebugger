using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebhookDebugger.Domain.Models
{
    public class Call
    {
        public Call(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            Method = httpContext.Request.Method;

            switch (httpContext.Request.Method)
            {
                case "PUT":
                case "POST":
                    using (var reader = new StreamReader(httpContext.Request.Body))
                    {
                        var task = reader.ReadToEndAsync();
                        Task.WaitAll(task);
                        Body = task.Result;
                    }

                    break;
            }

            QueryString = httpContext.Request.QueryString.ToString();
            Url = httpContext.Request.Path.ToString();
            Received = DateTime.Now;
            Headers = new List<Header>();
            foreach (var (key, value) in httpContext.Request.Headers)
            {
                Headers.Add(new Header
                    {
                        Name = key,
                        Value = value
                    });
            }
        }

        public string QueryString { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public List<Header> Headers { get; set; }
        public string Method { get; set; }
        public DateTime Received { get; set; }
    }
}
