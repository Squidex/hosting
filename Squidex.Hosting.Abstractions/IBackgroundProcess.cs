// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.Threading;
using System.Threading.Tasks;

namespace Squidex.Hosting
{
    public interface IBackgroundProcess : ISystem
    {
        Task StartAsync(CancellationToken ct);

        Task StopAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
