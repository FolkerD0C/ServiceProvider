using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;

namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Can build a service provider with support for scoped, singleton, and transient lifetimes.
/// </summary>
public interface IServiceProviderBuilder
{
    /// <summary>
    /// Registers a scoped service with the specified implementation type and expiration scope.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="scope">The expiration scope associated with the service.</param>
    /// <param name="instantiator">An optional factory method to create instances of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddScoped<TImplementation>(IServiceScope scope, Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a scoped service with a contract type and an implementation type, and associates it with an expiration scope.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="scope">The expiration scope associated with the service.</param>
    /// <param name="instantiator">An optional factory method to create instances of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotAssignableException">
    /// Thrown if TImplementation is not assignable to TContract.</exception>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddScoped<TContract, TImplementation>(IServiceScope scope, Func<TContract>? instantiator = null);

    /// <summary>
    /// Registers a singleton service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create a single instance of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a singleton service with a contract type and an implementation type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create a single instance of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotAssignableException">
    /// Thrown if TImplementation is not assignable to TContract.</exception>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator = null);

    /// <summary>
    /// Registers a transient service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create
    /// new instances of the service each time it is resolved.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a transient service with a contract type and an implementation type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create
    /// new instances of the service each time it is resolved.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotAssignableException">
    /// Thrown if TImplementation is not assignable to TContract.</exception>
    /// <exception cref="Exceptions.ServiceTypeNotInstantiatableException">
    /// Thrown if TImplementation is not instantiatable
    /// (eg. it is an interface or an abstract class).</exception>
    IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator = null);

    /// <summary>
    /// Builds the service provider, finalizing the service registrations and creating an <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <returns>An <see cref="IServiceProvider"/> instance with the configured services.</returns>
    IServiceProvider Build();

    /// <summary>
    /// Configures the builder using the specified <paramref name="builderConfiguration"/>.
    /// </summary>
    /// <param name="builderConfiguration">The configuration to apply.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilder"/>.</returns>
    IServiceProviderBuilder Configure(IServiceProviderBuilderConfiguration builderConfiguration);

    /// <summary>
    /// Configures the builder using all implementations of 
    /// <see cref="IServiceProviderBuilderConfiguration"/> found in the given <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to scan for configuration implementations.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilder"/>.</returns>
    IServiceProviderBuilder ConfigureFromAssembly(Assembly assembly);

    /// <summary>
    /// Configures the builder using all implementations of 
    /// <see cref="IServiceProviderBuilderConfiguration"/> found in the specified <paramref name="assemblies"/>.
    /// </summary>
    /// <param name="assemblies">An array of assemblies to scan for configuration implementations.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilder"/>.</returns>
    IServiceProviderBuilder ConfigureFromAssemblies(params Assembly[] assemblies);
}