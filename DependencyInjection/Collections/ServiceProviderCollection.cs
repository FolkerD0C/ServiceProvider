using FolkerD0C.DependencyInjection.Exceptions;

namespace FolkerD0C.DependencyInjection.Collections;

public class ServiceProviderCollection : IServiceProviderCollection
{
    #region Static
    public static readonly ServiceProviderCollection DefaultProviderCollection = new();

    public static IServiceProvider DefaultProvider
        => ServiceProvider.DefaultInstance;
    #endregion

    private readonly Dictionary<object, IServiceProvider> _serviceProviders = [];

    internal ServiceProviderCollection()
    {
    }

    public void AddServiceProvider(object key, IServiceProvider serviceProvider)
    {
        if (_serviceProviders.TryGetValue(key, out IServiceProvider? _))
        {
            throw new ServiceProviderAlreadyExistsException(key);
        }
        _serviceProviders.Add(key, serviceProvider);
    }

    public IServiceProvider GetServiceProvider(object key)
    {
        if (!_serviceProviders.TryGetValue(key, out IServiceProvider? serviceProvider))
        {
            throw new ServiceProviderNotFoundException(key);
        }
        return serviceProvider;
    }
}
