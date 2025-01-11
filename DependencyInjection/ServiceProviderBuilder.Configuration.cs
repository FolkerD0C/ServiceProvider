using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection;

public sealed partial class ServiceProviderBuilder
{
    public static IServiceProviderBuilder Configure(IServiceProviderBuilder builder, IServiceProviderBuilderConfiguration configuration)
    {
        return configuration.Configure(builder);
    }

    public static IServiceProviderBuilder ConfigureDefault(IServiceProviderBuilderConfiguration configuration)
    {
        return configuration.Configure(DefaultBuilder);
    }

    public static IServiceProviderBuilder ConfigureFromAssembly(IServiceProviderBuilder builder, Assembly assembly)
    {
        var configurationTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(iface => iface.Name.EndsWith(nameof(IServiceProviderBuilderConfiguration))));
        
        foreach (var configurationType in configurationTypes)
        {
            var configurationInstance = Activator.CreateInstance(configurationType);
            if (configurationInstance is IServiceProviderBuilderConfiguration configuration)
            {
                builder = Configure(builder, configuration);
            }
        }

        return builder;
    }

    public static IServiceProviderBuilder ConfigureDefaultFromAssembly(Assembly assembly)
    {
        return ConfigureFromAssembly(DefaultBuilder, assembly);
    }

    public static IServiceProviderBuilder ConfigureFromAssemblies(IServiceProviderBuilder builder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            builder = ConfigureFromAssembly(builder, assembly);
        }

        return builder;
    }

    public static IServiceProviderBuilder ConfigureDefaultFromAssemblies(params Assembly[] assemblies)
    {
        return ConfigureFromAssemblies(DefaultBuilder, assemblies);
    }
}