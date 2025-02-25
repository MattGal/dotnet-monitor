﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Configuration;

namespace Microsoft.Diagnostics.Tools.Monitor.Egress.Configuration
{
    /// <summary>
    /// Provides access to the Egress:Properties section of the configuration.
    /// </summary>
    internal class EgressPropertiesConfigurationProvider :
        IEgressPropertiesConfigurationProvider
    {
        public EgressPropertiesConfigurationProvider(IConfiguration configuration)
        {
            Configuration = configuration.GetEgressPropertiesSection();
        }

        /// <inheritdoc/>
        public IConfiguration Configuration { get; }
    }
}
