using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection.Collections;

public sealed partial class ServiceProviderBuilderCollection
{
    public static IServiceProviderBuilderCollection Configure(
        IServiceProviderBuilderCollection builderCollection,
        IServiceProviderBuilderCollectionConfiguration configuration)
    {
        return configuration.ConfigureCollection(builderCollection);
    }

    public static IServiceProviderBuilderCollection ConfigureDefault(
        IServiceProviderBuilderCollectionConfiguration configuration)
    {
        return Configure(DefaultBuilderCollection, configuration);
    }

    public static IServiceProviderBuilderCollection ConfigureFromAssembly(
        IServiceProviderBuilderCollection builderCollection, Assembly assembly)
    {
        var configurationTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(iface => iface.Name.EndsWith(nameof(IServiceProviderBuilderCollectionConfiguration))));
        
        foreach (var configurationType in configurationTypes)
        {
            var configurationInstance = Activator.CreateInstance(configurationType);
            if (configurationInstance is IServiceProviderBuilderCollectionConfiguration configuration)
            {
                builderCollection = Configure(builderCollection, configuration);
            }
        }

        return builderCollection;
    }

    public static IServiceProviderBuilderCollection ConfigureDefaultFromAssembly(
        Assembly assembly)
    {
        return ConfigureFromAssembly(DefaultBuilderCollection, assembly);
    }


    public static IServiceProviderBuilderCollection ConfigureFromAssemblies(
        IServiceProviderBuilderCollection builderCollection, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            builderCollection = ConfigureFromAssembly(builderCollection, assembly);
        }

        return builderCollection;
    }

    public static IServiceProviderBuilderCollection ConfigureDefaultFromAssemblies(
        params Assembly[] assemblies)
    {
        return ConfigureFromAssemblies(DefaultBuilderCollection, assemblies);
    }
}