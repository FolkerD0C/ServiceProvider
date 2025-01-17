using FluentAssertions;
using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Tests.Configurations.One;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests;

[Collection("All tests")]
public class ServiceProviderBuilderCollectionConfigurationShould : TestBase
{
    [Fact]
    public void BeConfiguredBySimpleConfiguration()
    {
        Random rng = new();
        int expectedProviderCount = rng.Next(20, 30);
        var keys = Enumerable.Range(0, expectedProviderCount)
            .Select(_ => Guid.NewGuid()).ToList();
        IServiceProviderBuilderCollection builderCollection = new ServiceProviderBuilderCollection();
        
        foreach (var key in keys)
        {
            builderCollection = builderCollection.Configure(
                new GetterCollectionConfiguration<Guid>(key));
        }
        var providerCollection = builderCollection.BuildAll();

        foreach (var key in keys)
        {
            providerCollection.GetServiceProvider(key)
                .Resolve<GetterService<Guid>>()
                .GetValue()
                .Should()
                .Be(key);
        }
    }

    [Fact]
    public void BeConfiguredFromAssembly()
    {
        var providerCollection = new ServiceProviderBuilderCollection()
            .ConfigureFromAssembly(typeof(ConfigurationAssembly.One.AssemblyReference).Assembly)
            .BuildAll();
        
        providerCollection.GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyOne)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyOne);
        
        providerCollection.GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyTwo)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyTwo);
    }

    [Fact]
    public void BeConfiguredFromAssemblies()
    {
        var providerCollection = new ServiceProviderBuilderCollection()
            .ConfigureFromAssemblies(
                typeof(ConfigurationAssemblies.One.AssemblyReference).Assembly,
                typeof(ConfigurationAssemblies.Two.AssemblyReference).Assembly)
            .BuildAll();
        
        providerCollection.GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyOne)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyOne);
        
        providerCollection.GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyTwo)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyTwo);
    }

#if CAN_RESET_GLOBAL_STATE
    [Fact]
    public void BeConfiguredFromAssemblyWithDefaultBuilderCollection()
    {
        ResetGlobalState();
        ServiceProviderBuilderCollection.ConfigureDefaultFromAssembly(
                typeof(ConfigurationAssembly.One.AssemblyReference).Assembly);
        ServiceProviderBuilderCollection.DefaultBuilderCollection.BuildAll();
        
        ServiceProviderCollection.DefaultProviderCollection
            .GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyOne)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyOne);
        
        ServiceProviderCollection.DefaultProviderCollection
            .GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyTwo)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyTwo);
    }

    [Fact]
    public void BeConfiguredBySimpleConfigurationWithDefaultBuilderCollection()
    {
        ResetGlobalState();
        Random rng = new();
        int expectedProviderCount = rng.Next(20, 30);
        var keys = Enumerable.Range(0, expectedProviderCount)
            .Select(_ => Guid.NewGuid()).ToList();
        
        foreach (var key in keys)
        {
            ServiceProviderBuilderCollection.ConfigureDefault(
                new GetterCollectionConfiguration<Guid>(key));
        }
        ServiceProviderBuilderCollection.DefaultBuilderCollection.BuildAll();

        foreach (var key in keys)
        {
            ServiceProviderCollection.DefaultProviderCollection
                .GetServiceProvider(key)
                .Resolve<GetterService<Guid>>()
                .GetValue()
                .Should()
                .Be(key);
        }
    }

    [Fact]
    public void BeConfiguredFromAssembliesWithDefaultBuilderCollection()
    {
        ResetGlobalState();
        ServiceProviderBuilderCollection.ConfigureDefaultFromAssemblies(
            typeof(ConfigurationAssemblies.One.AssemblyReference).Assembly,
            typeof(ConfigurationAssemblies.Two.AssemblyReference).Assembly);
        
        ServiceProviderBuilderCollection.DefaultBuilderCollection.BuildAll();
        
        ServiceProviderCollection.DefaultProviderCollection
            .GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyOne)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyOne);
        
        ServiceProviderCollection.DefaultProviderCollection
            .GetServiceProvider(ServiceProviderKeys.ServiceProviderKeyTwo)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(ServiceProviderKeys.ServiceProviderKeyTwo);
    }
#endif
}
