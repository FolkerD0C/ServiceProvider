using FolkerD0C.DependencyInjection.Exceptions;

namespace FolkerD0C.DependencyInjection.Collections;

/// <summary>
/// <inheritdoc cref="IServiceProviderCollection"/><br/>
/// Can only be created through <see cref="IServiceProviderBuilderCollection.BuildAll"/>.
/// </summary>
public sealed class ServiceProviderCollection : IServiceProviderCollection
{
    #region Static
    /// <summary>
    /// The default service provider collection.
    /// </summary>
    public static
#if !CAN_RESET_GLOBAL_STATE
    readonly
#endif
    IServiceProviderCollection DefaultProviderCollection = new ServiceProviderCollection();

    /// <summary>
    /// The default service provider (the same as <see cref="ServiceProvider.DefaultProvider"/>).
    /// </summary>
    public static IServiceProvider DefaultProvider => ServiceProvider.DefaultProvider;
    #endregion

    private readonly Dictionary<object, IServiceProvider> _serviceProviders = [];

    internal ServiceProviderCollection()
    {
    }

    /// <inheritdoc/>
    public void AddServiceProvider(object key, IServiceProvider serviceProvider)
    {
        if (key is null)
        {
            throw new InvalidServiceProviderKeyException(key);
        }
        if (serviceProvider is null)
        {
            throw new InvalidServiceProviderException(serviceProvider);
        }
        if (_serviceProviders.TryGetValue(key, out IServiceProvider? _))
        {
            throw new ServiceProviderAlreadyExistsException(key);
        }

        _serviceProviders.Add(key, serviceProvider);
    }

    /// <inheritdoc/>
    public IServiceProvider GetServiceProvider(object key)
    {
        if (key is null)
        {
            throw new InvalidServiceProviderKeyException(key);
        }
        if (!_serviceProviders.TryGetValue(key, out IServiceProvider? serviceProvider))
        {
            throw new ServiceProviderNotFoundException(key);
        }

        return serviceProvider;
    }

    /// <inheritdoc/>
    public IEnumerable<IServiceProvider> GetServiceProviders()
    {
        return _serviceProviders.Select(kvp => kvp.Value);
    }

    /// <inheritdoc/>
    public bool TryAddServiceProvider(object key, IServiceProvider serviceProvider)
    {
        bool serviceAdded = false;
        try
        {
            AddServiceProvider(key, serviceProvider);
            serviceAdded = true;
        }
        catch (DependencyInjectionBaseException)
        {
        }

        return serviceAdded;
    }

#if CAN_RESET_GLOBAL_STATE
    /// <summary>
    /// A method used in tests to reset static defaults
    /// </summary>
    public static void ResetGlobalState()
    {
        DefaultProviderCollection = new ServiceProviderCollection();
    }
#endif
}
