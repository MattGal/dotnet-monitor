﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Diagnostics.Monitoring.WebApi.Stacks;
using Microsoft.Diagnostics.NETCore.Client;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Diagnostics.Monitoring.WebApi
{
    internal static class StackUtilities
    {
        public static string GenerateStacksFilename(IEndpointInfo endpointInfo, bool plainText)
        {
            string extension = plainText ? "txt" : "json";
            return FormattableString.Invariant($"{Utilities.GetFileNameTimeStampUtcNow()}_{endpointInfo.ProcessId}.stacks.{extension}");
        }

        public static async Task CollectStacksAsync(TaskCompletionSource<object> startCompletionSource,
            IEndpointInfo endpointInfo,
            ProfilerChannel profilerChannel,
            bool plainText,
            Stream outputStream, CancellationToken token)
        {
            var settings = new EventStacksPipelineSettings
            {
                Duration = Timeout.InfiniteTimeSpan
            };
            await using var eventTracePipeline = new EventStacksPipeline(new DiagnosticsClient(endpointInfo.Endpoint), settings);

            Task runPipelineTask = await eventTracePipeline.StartAsync(token);

            //CONSIDER Should we set this before or after the profiler message has been sent.
            startCompletionSource?.TrySetResult(null);

            ProfilerMessage response = await profilerChannel.SendMessage(
                endpointInfo,
                new ProfilerMessage { MessageType = ProfilerMessageType.Callstack, Parameter = 0 },
                token);

            if (response.MessageType == ProfilerMessageType.Error)
            {
                throw new InvalidOperationException($"Profiler request failed: 0x{response.Parameter:X8}");
            }
            await runPipelineTask;
            Stacks.CallStackResult result = await eventTracePipeline.Result;

            StacksFormatter formatter = (plainText == true) ? new TextStacksFormatter(outputStream) : new JsonStacksFormatter(outputStream);

            await formatter.FormatStack(result, token);
        }
    }
}
