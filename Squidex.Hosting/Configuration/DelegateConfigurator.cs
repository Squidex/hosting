// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using Microsoft.Extensions.Options;

namespace Squidex.Hosting.Configuration
{
    public sealed class DelegateConfigurator<T> : IConfigureOptions<T> where T : class
    {
        private readonly Action<IServiceProvider, T> configure;
        private readonly IServiceProvider serviceProvider;

        public DelegateConfigurator(Action<IServiceProvider, T> configure, IServiceProvider serviceProvider)
        {
            this.configure = configure;

            this.serviceProvider = serviceProvider;
        }

        public void Configure(T options)
        {
            configure(serviceProvider, options);
        }
    }
}
