﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

#if UNITTEST
namespace Microsoft.Diagnostics.Monitoring.TestCommon
#else
namespace Microsoft.Diagnostics.Tools.Monitor.Profiler
#endif
{
    public static class ProfilerIdentifiers
    {
        // Name of the monitor profiler library file without the
        // extra platform specific library naming conventions.
        public const string LibraryRootFileName = "MonitorProfiler";

        public static class Clsid
        {
            public const string StringWithDashes = "6A494330-5848-4A23-9D87-0E57BBF6DE79";
            public const string StringWithBraces = "{" + StringWithDashes + "}";

            public static readonly Guid Guid = new Guid(StringWithDashes);
        }

        public static class EnvironmentVariables
        {
            private const string ProfilerPrefix = ToolIdentifiers.StandardPrefix + "Profiler_";

            // This environment variable is automatically applied to a target process by the tool to inform
            // the profiler which directory it should use to share files and information with dotnet-monitor.
            public const string SharedPath = ProfilerPrefix + nameof(SharedPath);

            // This environment variable name is embedded into the profiler and set at profiler initialization.
            // The value is determined BEFORE native build by the generation of the product version into the
            // _productversion.h header file.
            public const string ProductVersion = ProfilerPrefix + nameof(ProductVersion);

            // This environment variable is automatically applied to a target process by the tool to inform
            // the profiler running in the target process the value of the runtime instance.
            public const string RuntimeInstanceId = ProfilerPrefix + nameof(RuntimeInstanceId);

            // (Optional) This environment variable is manually applied to the target process to override the
            // default level of the stderr logger.
            public const string StdErrLogger_Level = ProfilerPrefix + nameof(StdErrLogger_Level);
        }
    }
}
