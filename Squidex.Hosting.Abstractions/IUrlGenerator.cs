// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using Microsoft.AspNetCore.Http;

namespace Squidex.Hosting
{
    public interface IUrlGenerator
    {
        HostString BuildHost();

        string BuildCallbackUrl(string path, bool trailingSlash = true);

        string BuildUrl();

        string BuildUrl(string path, bool trailingSlash = true);

        bool IsAllowedHost(string? url);

        bool IsAllowedHost(Uri uri);
    }
}