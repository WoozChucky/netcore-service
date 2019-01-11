using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Events;
using Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyTransportLayer.Http
{
    internal class HttpStartup
    {
        internal static event EventHandler<RequestMessageEventArgs> Request;

        public HttpStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMvcCore();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            app.UseDeveloperExceptionPage();

            app.UseHsts();

            // app.UseHttpsRedirection();

            app.UseCors();

            app.UseMvc();

            app.Use(async (context, next) =>
            {
                var headers = new Dictionary<string, string>();
                foreach (var header in context.Request.Headers)
                {
                    headers.Add(header.Key, header.Value);
                }

                Request(this, new RequestMessageEventArgs
                {
                    CorrelationId = Guid.NewGuid(),
                    Request = new RequestMessage
                    {
                        Content = context.Request.Body.ToByteArray(),
                        Headers = headers,
                        Path = context.Request.Path
                    }
                });

                // Do work that doesn't write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });
        }
    }
}
