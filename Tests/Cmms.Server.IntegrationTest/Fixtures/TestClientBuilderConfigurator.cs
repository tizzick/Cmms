namespace Cmms.Server.IntegrationTest.Fixtures;

using Microsoft.Extensions.Configuration;
using Orleans;
using Orleans.Hosting;
using Orleans.TestingHost;
using Cmms.Abstractions.Constants;

/// <summary>
/// Configures the client builder for the test environment.
/// </summary>
public class TestClientBuilderConfigurator : IClientBuilderConfigurator
{
    /// <summary>
    /// Configures the client builder for the test environment.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="clientBuilder"></param>
    public void Configure(IConfiguration configuration, IClientBuilder clientBuilder) =>
        clientBuilder.AddSimpleMessageStreamProvider(StreamProviderName.Default);
}
