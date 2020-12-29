// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Squidex.Hosting
{
    public sealed class DelegateInitializer : IInitializable
    {
        private readonly Func<CancellationToken, Task> action;

        public string Name { get; }

        public DelegateInitializer(string name, Func<CancellationToken, Task> action)
        {
            Name = name;

            this.action = action;
        }

        public async Task InitializeAsync(CancellationToken ct = default)
        {
            if (action != null)
            {
                await action(ct);
            }
        }
    }
}
