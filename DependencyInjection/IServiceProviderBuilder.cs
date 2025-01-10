namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Provides a contract for building a service provider with support for scoped, singleton, and transient lifetimes.
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
    IServiceProviderBuilder AddScoped<TImplementation>(IServiceScope scope, Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a scoped service with a contract type and an implementation type, and associates it with an expiration scope.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="scope">The expiration scope associated with the service.</param>
    /// <param name="instantiator">An optional factory method to create instances of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    IServiceProviderBuilder AddScoped<TContract, TImplementation>(IServiceScope scope, Func<TContract>? instantiator = null);

    /// <summary>
    /// Registers a singleton service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create a single instance of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a singleton service with a contract type and an implementation type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create a single instance of the service.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator = null);

    /// <summary>
    /// Registers a transient service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create new instances of the service each time it is resolved.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator = null);

    /// <summary>
    /// Registers a transient service with a contract type and an implementation type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to register.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to register.</typeparam>
    /// <param name="instantiator">An optional factory method to create new instances of the service each time it is resolved.</param>
    /// <returns>The current <see cref="IServiceProviderBuilder"/> instance.</returns>
    IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator = null);

    /// <summary>
    /// Builds the service provider, finalizing the service registrations and creating an <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <returns>An <see cref="IServiceProvider"/> instance with the configured services.</returns>
    IServiceProvider Build();
}