﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Diagnostics.Monitoring.TestCommon;
using Microsoft.Diagnostics.Monitoring.TestCommon.Runners;
using Microsoft.Diagnostics.Monitoring.Tool.FunctionalTests.Fixtures;
using Microsoft.Diagnostics.Monitoring.Tool.FunctionalTests.HttpApi;
using Microsoft.Diagnostics.Monitoring.Tool.FunctionalTests.Runners;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Diagnostics.Monitoring.Tool.FunctionalTests
{
    [TargetFrameworkMonikerTrait(TargetFrameworkMonikerExtensions.CurrentTargetFrameworkMoniker)]
    [Collection(DefaultCollectionFixture.Name)]
    public class RootTests
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITestOutputHelper _outputHelper;

        public RootTests(ITestOutputHelper outputHelper, ServiceProviderFixture serviceProviderFixture)
        {
            _httpClientFactory = serviceProviderFixture.ServiceProvider.GetService<IHttpClientFactory>();
            _outputHelper = outputHelper;
        }

        /// <summary>
        /// Tests that the root route of the URLs will return HTTP 404
        /// </summary>
        [Fact]
        public async Task RootRoutesReturn404Test()
        {
            await using MonitorCollectRunner toolRunner = new(_outputHelper);
            await toolRunner.StartAsync();

            // Test default URL root returns HTTP 404
            using HttpClient defaultHttpClient = await toolRunner.CreateHttpClientDefaultAddressAsync(_httpClientFactory);
            ApiClient defaultApiClient = new(_outputHelper, defaultHttpClient);

            var statusCodeException = await Assert.ThrowsAsync<ApiStatusCodeException>(
                () => defaultApiClient.GetRootAsync());
            Assert.Equal(HttpStatusCode.NotFound, statusCodeException.StatusCode);

            // Test metrics URL root returns HTTP 404
            using HttpClient metricsHttpClient = await toolRunner.CreateHttpClientMetricsAddressAsync(_httpClientFactory);
            ApiClient metricsApiClient = new(_outputHelper, defaultHttpClient);

            statusCodeException = await Assert.ThrowsAsync<ApiStatusCodeException>(
                () => defaultApiClient.GetRootAsync());
            Assert.Equal(HttpStatusCode.NotFound, statusCodeException.StatusCode);
        }
    }
}
