namespace FolkerD0C.DependencyInjection.Collections;

/// <summary>
/// Represents a collection of service providers identified by unique keys.
/// </summary>
public interface IServiceProviderCollection : IResettable
{
    /// <summary>
    /// Adds a service provider to the collection with a specified key.
    /// </summary>
    /// <param name="key">The unique identifier for the service provider.</param>
    /// <param name="serviceProvider">The service provider to add to the collection.</param>
    /// <exception cref="Exceptions.InvalidServiceProviderKeyException">
    /// Thrown if the <paramref name="key"/> is null.</exception>
    /// <exception cref="Exceptions.InvalidServiceProviderException">
    /// Thrown if the <paramref name="serviceProvider"/> is null.</exception>
    /// <exception cref="Exceptions.ServiceProviderAlreadyExistsException">Thrown if a service
    /// provider with the same key already exists in the collection.</exception>
    void AddServiceProvider(object key, IServiceProvider serviceProvider);

    /// <summary>
    /// Retrieves a service provider associated with the specified key.
    /// </summary>
    /// <param name="key">The unique identifier of the service provider to retrieve.</param>
    /// <returns>
    /// The <see cref="IServiceProvider"/> associated with the specified key.
    /// </returns>
    /// <exception cref="Exceptions.InvalidServiceProviderKeyException">
    /// Thrown if the <paramref name="key"/> is null.</exception>
    /// <exception cref="Exceptions.ServiceProviderNotFoundException">
    /// Thrown if the <paramref name="key"/> is not in the collection.</exception>
    IServiceProvider GetServiceProvider(object key);

    /// <summary>
    /// Retrieves all service providers contained in the collection.
    /// </summary>
    /// <returns>An enumerable of all service providers in the collection.</returns>
    IEnumerable<IServiceProvider> GetServiceProviders();

    /// <summary>
    /// Attempts to add a service provider to the collection with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the service provider.</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to add to the collection.</param>
    /// <returns>
    /// <c>true</c> if the service provider was added successfully; 
    /// otherwise, <c>false</c> if a provider with the same key already exists
    /// or one of the parameters is invalid.
    /// </returns>
    bool TryAddServiceProvider(object key, IServiceProvider serviceProvider);
}
