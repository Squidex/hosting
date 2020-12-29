// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;

namespace Squidex.Hosting
{
    public static class UrlExtensions
    {
        public static string BuildFullUrl(this string baseUrl, string path, bool trailingSlash = false)
        {
            var url = $"{baseUrl.TrimEnd('/')}/{path.Trim('/')}";

            if (trailingSlash &&
                url.IndexOf("#", StringComparison.OrdinalIgnoreCase) < 0 &&
                url.IndexOf("?", StringComparison.OrdinalIgnoreCase) < 0 &&
                url.IndexOf(";", StringComparison.OrdinalIgnoreCase) < 0)
            {
                url += "/";
            }

            return url;
        }
    }
}
