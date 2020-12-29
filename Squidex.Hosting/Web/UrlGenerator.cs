// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Squidex.Hosting.Configuration;

namespace Squidex.Hosting.Web
{
    public sealed class UrlGenerator : IUrlGenerator
    {
        private readonly HashSet<HostString> allTrustedHosts = new HashSet<HostString>();
        private readonly UrlOptions options;

        public UrlGenerator(IOptions<UrlOptions> options)
        {
            this.options = options.Value;

            if (TryBuildHost(options.Value.BaseUrl, out var host1))
            {
                allTrustedHosts.Add(host1);
            }

            if (TryBuildHost(options.Value.CallbackUrl, out var host2))
            {
                allTrustedHosts.Add(host2);
            }

            if (options.Value.TrustedHosts != null)
            {
                foreach (var host in options.Value.TrustedHosts)
                {
                    if (TryBuildHost(host, out var host3))
                    {
                        allTrustedHosts.Add(host3);
                    }
                }
            }
        }

        public string BuildCallbackUrl(string path, bool trailingSlash = true)
        {
            if (string.IsNullOrWhiteSpace(options.CallbackUrl))
            {
                return BuildUrl(path, trailingSlash);
            }

            return options.BaseUrl.BuildFullUrl(path, trailingSlash);
        }

        public HostString BuildHost()
        {
            if (!TryBuildHost(options.BaseUrl, out var host))
            {
                var error = new ConfigurationError("urls:baseurl", "Value is required.");

                throw new ConfigurationException(error);
            }

            return host;
        }

        public string BuildUrl()
        {
            return options.BaseUrl;
        }

        public string BuildUrl(string path, bool trailingSlash = true)
        {
            return options.BaseUrl.BuildFullUrl(path, trailingSlash);
        }

        public bool IsAllowedHost(string? url)
        {
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            {
                return false;
            }

            return IsAllowedHost(uri);
        }

        public bool IsAllowedHost(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
            {
                return true;
            }

            return allTrustedHosts.Contains(BuildHost(uri));
        }

        private static bool TryBuildHost(string? urlOrHost, out HostString host)
        {
            host = default;

            if (string.IsNullOrWhiteSpace(urlOrHost))
            {
                return false;
            }

            if (Uri.TryCreate(urlOrHost, UriKind.Absolute, out var uri1))
            {
                host = BuildHost(uri1);

                return true;
            }

            if (Uri.TryCreate($"http://{urlOrHost}", UriKind.Absolute, out var uri2))
            {
                host = BuildHost(uri2);

                return true;
            }

            return false;
        }

        private static HostString BuildHost(Uri uri)
        {
            return BuildHost(uri.Host, uri.Port);
        }

        private static HostString BuildHost(string host, int port)
        {
            if (port == 443 || port == 80)
            {
                return new HostString(host.ToLowerInvariant());
            }
            else
            {
                return new HostString(host.ToLowerInvariant(), port);
            }
        }
    }
}
