namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Provides a contract for resolving services from a service container.
/// </summary>
public interface IServiceProvider
{
    /// <summary>
    /// Resolves a service instance of the specified contract type.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract to resolve.</typeparam>
    /// <returns>An instance of the specified contract type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no service of the specified type is registered or cannot be resolved.
    /// </exception>
    TContract Resolve<TContract>();
}