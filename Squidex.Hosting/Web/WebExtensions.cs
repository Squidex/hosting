// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Squidex.Hosting.Web
{
    public static class WebExtensions
    {
        public static void UseDefaultForwardingRules(this IApplicationBuilder app)
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
