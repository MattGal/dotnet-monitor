﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tools.Monitor
{
    public class HostBuilderResults
    {
        public const string ResultKey = "DotnetMonitorHostBuilderResults";

        public List<string> Warnings { get; } = new();
    }
}
