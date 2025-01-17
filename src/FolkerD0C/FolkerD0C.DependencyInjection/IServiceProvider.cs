namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Provides services that are registered in an <see cref="IServiceProviderBuilder"/>.
/// </summary>
public interface IServiceProvider
{
    /// <summary>
    /// Retrieves all currently registered services.
    /// </summary>
    /// <returns>An enumerable collection of registered service types.</returns>
    IEnumerable<Type> GetRegisteredServiceTypes();

    /// <summary>
    /// Resolves a service instance of the specified contract type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to resolve.</typeparam>
    /// <returns>An instance of the specified contract type.</returns>
    /// <exception cref="Exceptions.ServiceTypeNotRegisteredException">
    /// Thrown if no service of the specified type is registered or cannot be resolved.
    /// </exception>
    TContract Resolve<TContract>();

    /// <summary>
    /// Resolves a service instance of the specified contract type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to resolve.</typeparam>
    /// <param name="serviceInstance">The result of the operation as an out parameter.</param>
    /// <returns>True if the service resolution and instantiation succeeded else false.</returns>
    bool GetService<TContract>(out TContract? serviceInstance);
}