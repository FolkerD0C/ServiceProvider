using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Utilities;

namespace FolkerD0C.DependencyInjection.Collections;

public class ServiceProviderBuilderCollection
{
    #region Static
    public static IServiceProviderBuilder DefaultBuilder => ServiceProviderBuilder.DefaultInstance;
    public static readonly ServiceProviderBuilderCollection DefaultBuilderCollection = new(ServiceProviderCollection.DefaultProviderCollection);
    #endregion

    private readonly Dictionary<object, IServiceProviderBuilder> _serviceProviderBuilders = [];
    private readonly ServiceProviderCollection _providerCollection;

    private bool _serviceProvidersBuilt = false;

    private ServiceProviderBuilderCollection(ServiceProviderCollection providerCollection)
    {
        _providerCollection = providerCollection;
    }

    public ServiceProviderBuilderCollection() : this(new())
    {
    }

    public ServiceProviderCollection BuildAll()
    {
        if (_serviceProvidersBuilt)
        {
            throw new ServiceProvidersHaveBeenBuiltException();
        }

        foreach (var keyAndBuilder in _serviceProviderBuilders)
        {
            _providerCollection.AddServiceProvider(keyAndBuilder.Key, keyAndBuilder.Value.Build());
        }
        _serviceProvidersBuilt = true;
        return _providerCollection;
    }

    public IServiceProviderBuilder GetBuilder(object key)
    {
        if (_serviceProvidersBuilt)
        {
            throw new ServiceProvidersHaveBeenBuiltException();
        }

        if (key is null || key.Equals(Constants.DefaultServiceProviderKey))
        {
            throw new InvalidServiceProviderKeyException(key);
        }

        if  (_serviceProviderBuilders.TryGetValue(key, out IServiceProviderBuilder? builder))
        {
            return builder;
        }

        builder = new ServiceProviderBuilder();
        _serviceProviderBuilders.Add(key, builder);
        return builder;
    }
}
