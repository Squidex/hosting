﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.Threading;
using System.Threading.Tasks;

namespace Squidex.Hosting
{
    public interface IInitializable : ISystem
    {
        Task InitializeAsync(CancellationToken ct);

        Task ReleaseAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
