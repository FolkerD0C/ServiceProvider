using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Exceptions;

namespace FolkerD0C.DependencyInjection.Collections;

public sealed partial class ServiceProviderBuilderCollection : IServiceProviderBuilderCollection
{
    #region Static
    /// <summary>
    /// The default service provider builder collection.
    /// </summary>
    public static readonly IServiceProviderBuilderCollection DefaultBuilderCollection =
        new ServiceProviderBuilderCollection(ServiceProviderCollection.DefaultProviderCollection);

    /// <summary>
    /// The default service provider builder (the same as <see cref="ServiceProviderBuilder.DefaultBuilder"/>).
    /// </summary>
    public static IServiceProviderBuilder DefaultBuilder => ServiceProviderBuilder.DefaultBuilder;
    #endregion

    private readonly Dictionary<object, IServiceProviderBuilder> _serviceProviderBuilders = [];
    private IServiceProviderCollection? _providerCollection;

    private bool _serviceProvidersBuilt = false;

    private ServiceProviderBuilderCollection(IServiceProviderCollection providerCollection)
    {
        _providerCollection = providerCollection;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ServiceProviderBuilderCollection"/> class.
    /// </summary>
    public ServiceProviderBuilderCollection() : this(new ServiceProviderCollection())
    {
    }

    /// <inheritdoc/>
    public IServiceProviderCollection BuildAll()
    {
        if (_serviceProvidersBuilt || _providerCollection is null)
        {
            throw new ServiceProvidersHaveBeenBuiltException();
        }

        foreach (var keyAndBuilder in _serviceProviderBuilders)
        {
            _providerCollection.AddServiceProvider(keyAndBuilder.Key, keyAndBuilder.Value.Build());
        }
        _serviceProvidersBuilt = true;
        IServiceProviderCollection result = _providerCollection;
        Reset(false);
        return result;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilderCollection Configure(IServiceProviderBuilderCollectionConfiguration configuration)
    {
        return configuration.ConfigureBuilderCollection(this);
    }

    /// <inheritdoc/>
    public IServiceProviderBuilderCollection ConfigureFromAssembly(Assembly assembly)
    {
        var configurationTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(iface => iface.Name.EndsWith(nameof(IServiceProviderBuilderCollectionConfiguration))));
        
        foreach (var configurationType in configurationTypes)
        {
            var configurationInstance = Activator.CreateInstance(configurationType);
            if (configurationInstance is IServiceProviderBuilderCollectionConfiguration configuration)
            {
                Configure(configuration);
            }
        }

        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilderCollection ConfigureFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ConfigureFromAssembly(assembly);
        }

        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder GetBuilder(object key)
    {
        if (_serviceProvidersBuilt)
        {
            throw new ServiceProvidersHaveBeenBuiltException();
        }

        if (key is null)
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

    /// <inheritdoc/>
    public void Reset()
    {
        Reset(true);
    }

    void Reset(bool externalReset)
    {
        if (_serviceProvidersBuilt)
        {
            _providerCollection = new ServiceProviderCollection();
        }
        _serviceProviderBuilders.Clear();
        if (externalReset)
        {
            _serviceProvidersBuilt = false;
            _providerCollection?.Reset();
        }
    }
}
