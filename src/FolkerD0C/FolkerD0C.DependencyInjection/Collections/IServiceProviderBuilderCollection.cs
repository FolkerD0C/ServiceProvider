using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection.Collections;

/// <summary>
/// Represents a collection of service provider builders that can construct a collection of service providers.
/// </summary>
public interface IServiceProviderBuilderCollection : IResettable
{
    /// <summary>
    /// Builds and returns a collection of service providers from the builders in the collection.
    /// After building the providers it disposes the builder collection.
    /// </summary>
    /// <returns>
    /// An <see cref="IServiceProviderCollection"/> containing all service providers
    /// built from the builders in the collection.
    /// </returns>
    /// <exception cref="Exceptions.ServiceProvidersHaveBeenBuiltException">
    /// Thrown if this method has already been called.
    /// </exception>
    IServiceProviderCollection BuildAll();

    /// <summary>
    /// Configures the builder collection using the provided 
    /// <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">The configuration to apply to the builder collection.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilderCollection"/>.</returns>
    IServiceProviderBuilderCollection Configure(IServiceProviderBuilderCollectionConfiguration configuration);

    /// <summary>
    /// Configures the builder collection using all implementations of 
    /// <see cref="IServiceProviderBuilderCollectionConfiguration"/> found in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to scan for configuration implementations.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilderCollection"/>.</returns>
    IServiceProviderBuilderCollection ConfigureFromAssembly(Assembly assembly);

    /// <summary>
    /// Configures the builder collection using all implementations of 
    /// <see cref="IServiceProviderBuilderCollectionConfiguration"/> found in the specified <paramref name="assemblies"/>.
    /// </summary>
    /// <param name="assemblies">An array of assemblies to scan for configuration implementations.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilderCollection"/>.</returns>
    IServiceProviderBuilderCollection ConfigureFromAssemblies(params Assembly[] assemblies);

    /// <summary>
    /// Retrieves a builder associated with the specified key.
    /// </summary>
    /// <param name="key">The unique identifier of the builder to retrieve.</param>
    /// <returns>
    /// The <see cref="IServiceProviderBuilder"/> associated with the specified key.
    /// If it does not exists then it gets created on the fly.
    /// </returns>
    /// <exception cref="Exceptions.InvalidServiceProviderKeyException">
    /// Thrown if the <paramref name="key"/> is null.</exception>
    /// <exception cref="Exceptions.ServiceProvidersHaveBeenBuiltException">
    /// Thrown if <see cref="BuildAll"/> has already been called.
    /// </exception>
    IServiceProviderBuilder GetBuilder(object key);
}
