using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebhookDebugger.Api.Middleware;
using WebhookDebugger.Application.Services;
using WebhookDebugger.Domain.Services;
using WebhookDebugger.Infrastructure;

namespace WebhookDebugger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCorrelationId();

            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionLogging>();

            app.UseCorrelationId(new CorrelationIdOptions
            {
                Header = "x-correlation-id",
                UseGuidForCorrelationId = true,
                IncludeInResponse = true,
                UpdateTraceIdentifier = true
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
