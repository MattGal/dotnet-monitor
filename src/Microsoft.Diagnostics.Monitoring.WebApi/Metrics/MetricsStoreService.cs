﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Diagnostics.Monitoring.WebApi
{
    internal sealed class MetricsStoreService : IDisposable
    {
        public MetricsStore MetricsStore { get; }

        public MetricsStoreService(
            IOptions<MetricsOptions> options)
        {
            MetricsStore = new MetricsStore(options.Value.MetricCount.GetValueOrDefault(MetricsOptionsDefaults.MetricCount));
        }

        public void Dispose()
        {
            MetricsStore.Dispose();
        }
    }
}
