// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squidex.Log;

namespace Squidex.Hosting
{
    public sealed class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingletonAs(_ => JsonLogWriterFactory.Readable())
                .As<IObjectWriterFactory>();

            services.AddSingletonAs(_ => new ConsoleLogChannel())
                .As<ILogChannel>();

            services.AddDefaultForwardRules();
            services.AddDefaultWebServices(configuration);

            services.AddSingletonAs<MyService1>();
            services.AddSingletonAs<MyService2>();

            services.AddInitializer();
            services.AddBackgroundProcesses();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultPathBase();
            app.UseDefaultForwardRules();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hello", async context =>
                {
                    await context.Response.WriteAsync("Hello, World!");
                });
            });
        }
    }
}