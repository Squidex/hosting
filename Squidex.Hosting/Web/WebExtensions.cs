// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Squidex.Hosting;
using Squidex.Hosting.Web;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebExtensions
    {
        public static void UseDefaultPathBase(this IApplicationBuilder app)
        {
            var urlGenerator = app.ApplicationServices.GetRequiredService<IUrlGenerator>();

            var basePath = urlGenerator.BuildBasePath();

            app.UsePathBase(basePath);
        }

        public static void UseDefaultForwardRules(this IApplicationBuilder app)
        {
            var urlsOptions = app.ApplicationServices.GetRequiredService<IOptions<UrlOptions>>().Value;

            if (urlsOptions.EnableForwardHeaders)
            {
                app.UseForwardedHeaders();
            }

            app.UseMiddleware<CleanupHostMiddleware>();

            if (urlsOptions.EnforceHost)
            {
                app.UseHostFiltering();
            }

            if (urlsOptions.EnforceHTTPS)
            {
                app.UseHttpsRedirection();
            }
        }
    }
}
