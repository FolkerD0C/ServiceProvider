using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection;

public sealed partial class ServiceProviderBuilder
{
    /// <summary>
    /// Configures the default <see cref="IServiceProviderBuilder"/> using the specified 
    /// <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">The configuration to apply to the default builder.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilder"/>.</returns>
    public static IServiceProviderBuilder ConfigureDefault(IServiceProviderBuilderConfiguration configuration)
    {
        return DefaultBuilder.Configure(configuration);
    }

    /// <summary>
    /// Configures the default <see cref="IServiceProviderBuilder"/> using all implementations of 
    /// <see cref="IServiceProviderBuilderConfiguration"/> found in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to scan for configuration implementations.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilder"/>.</returns>
    public static IServiceProviderBuilder ConfigureDefaultFromAssembly(Assembly assembly)
    {
        return DefaultBuilder.ConfigureFromAssembly(assembly);
    }

    /// <summary>
    /// Configures the default <see cref="IServiceProviderBuilder"/> using all implementations of 
    /// <see cref="IServiceProviderBuilderConfiguration"/> found in the specified <paramref name="assemblies"/>.
    /// </summary>
    /// <param name="assemblies">An array of assemblies to scan for configuration implementations.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilder"/>.</returns>
    public static IServiceProviderBuilder ConfigureDefaultFromAssemblies(params Assembly[] assemblies)
    {
        return DefaultBuilder.ConfigureFromAssemblies(assemblies);
    }
}