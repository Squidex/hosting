// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.Extensions.Configuration;
using Squidex.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HostingServiceExtensions
    {
        public static IServiceCollection AddInitializer(this IServiceCollection services)
        {
            services.AddOptionValidation();

            return services.AddHostedService<InitializerHost>();
        }

        public static IServiceCollection AddBackgroundProcesses(this IServiceCollection services)
        {
            return services.AddHostedService<BackgroundHost>();
        }
    }
}
