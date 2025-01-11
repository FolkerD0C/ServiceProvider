using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection.Collections;

public sealed partial class ServiceProviderBuilderCollection
{
    /// <summary>
    /// Configures the default <see cref="IServiceProviderBuilderCollection"/> using the specified 
    /// <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">The configuration to apply to the default builder collection.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilderCollection"/>.</returns>
    public static IServiceProviderBuilderCollection ConfigureDefault(
        IServiceProviderBuilderCollectionConfiguration configuration)
    {
        return DefaultBuilderCollection.Configure(configuration);
    }

    /// <summary>
    /// Configures the default <see cref="IServiceProviderBuilderCollection"/> using all implementations of 
    /// <see cref="IServiceProviderBuilderCollectionConfiguration"/> found in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to scan for configuration implementations.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilderCollection"/>.</returns>
    public static IServiceProviderBuilderCollection ConfigureDefaultFromAssembly(
        Assembly assembly)
    {
        return DefaultBuilderCollection.ConfigureFromAssembly(assembly);
    }

    /// Configures the default <see cref="IServiceProviderBuilderCollection"/> using all implementations of 
    /// <see cref="IServiceProviderBuilderCollectionConfiguration"/> found in the specified <paramref name="assemblies"/>.
    /// </summary>
    /// <param name="assemblies">An array of assemblies to scan for configuration implementations.</param>
    /// <returns>The configured default <see cref="IServiceProviderBuilderCollection"/>.</returns>
    public static IServiceProviderBuilderCollection ConfigureDefaultFromAssemblies(
        params Assembly[] assemblies)
    {
        return DefaultBuilderCollection.ConfigureFromAssemblies(assemblies);
    }
}