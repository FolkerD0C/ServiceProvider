using FluentAssertions;
using FolkerD0C.DependencyInjection.Tests.AssemblyTests.One;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderBuilderConfigurationShould : TestBase
{
    [Fact]
    public void BeConfiguredBySimpleConfiguration()
    {
        var key = Guid.NewGuid();
        var builder = new ServiceProviderBuilder();
        var configuration = new GetterConfiguration<Guid>(key);

        var provider = ServiceProviderBuilder.Configure(builder, configuration).Build();

        provider.Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(key);
    }

    [Fact]
    public void BeConfiguredBySimpleConfigurationWithDefaultBuilder()
    {
        ResetGlobalState();
        var key = Guid.NewGuid();
        var configuration = new GetterConfiguration<Guid>(key);

        ServiceProviderBuilder.ConfigureDefault(configuration);

        ServiceProvider.BuildDefaultProvider();

        ServiceProvider.DefaultProvider.Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(key);
    }

    [Fact]
    public void BeConfiguredFromAssembly()
    {
        var builder = new ServiceProviderBuilder();

        var provider = ServiceProviderBuilder.ConfigureFromAssembly(builder,
            typeof(ConfigurationAssembly.One.AssemblyReference).Assembly).Build();
        
        provider.Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.GuidResponse);
        
        provider.Resolve<GetterService<string>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.StringResponse);
    }

    [Fact]
    public void BeConfiguredFromAssemblyWithDefaultBuilder()
    {
        ResetGlobalState();

        ServiceProviderBuilder.ConfigureDefaultFromAssembly(
            typeof(ConfigurationAssembly.One.AssemblyReference).Assembly).Build();
        
        ServiceProvider.BuildDefaultProvider();
        
        ServiceProvider.DefaultProvider.Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.GuidResponse);
        
        ServiceProvider.DefaultProvider.Resolve<GetterService<string>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.StringResponse);
    }

    [Fact]
    public void BeConfiguredFromAssemblies()
    {
        var builder = new ServiceProviderBuilder();
        var provider = ServiceProviderBuilder.ConfigureFromAssemblies(builder,
            typeof(ConfigurationAssemblies.One.AssemblyReference).Assembly,
            typeof(ConfigurationAssemblies.Two.AssemblyReference).Assembly).Build();
        
        provider.Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.GuidResponse);
        
        provider.Resolve<GetterService<string>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.StringResponse);
    }

    [Fact]
    public void BeConfiguredFromAssembliesWithDefaultBuilder()
    {
        ResetGlobalState();
        ServiceProviderBuilder.ConfigureDefaultFromAssemblies(
            typeof(ConfigurationAssemblies.One.AssemblyReference).Assembly,
            typeof(ConfigurationAssemblies.Two.AssemblyReference).Assembly);
        
        ServiceProvider.BuildDefaultProvider();

        ServiceProvider.DefaultProvider
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.GuidResponse);

        ServiceProvider.DefaultProvider
            .Resolve<GetterService<string>>()
            .GetValue()
            .Should()
            .Be(ServiceResponses.StringResponse);
    }
}
