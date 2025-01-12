using System.Reflection;
using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Utilities;

namespace FolkerD0C.DependencyInjection;

/// <inheritdoc cref="IServiceProviderBuilder"/>
public sealed partial class ServiceProviderBuilder : IServiceProviderBuilder
{
    /// <summary>
    /// The default service provider builder.
    /// </summary>
    public static readonly IServiceProviderBuilder DefaultBuilder = new ServiceProviderBuilder();

    private readonly Dictionary<Type, RegisteredType> _registeredTypes = [];

    #region Public methods
    /// <inheritdoc/>
    public IServiceProviderBuilder AddScoped<TImplementation>(IServiceScope scope, Func<TImplementation>? instantiator = null)
    {
        Add(typeof(TImplementation), scope, instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder AddScoped<TContract, TImplementation>(IServiceScope scope, Func<TContract>? instantiator = null)
    {
        Add(typeof(TImplementation), scope, instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator = null)
    {
        Add(typeof(TImplementation), ServiceScope.GetSingleton(), instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator = null)
    {
        Add(typeof(TImplementation), ServiceScope.GetSingleton(), instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator = null)
    {
        Add(typeof(TImplementation), ServiceScope.GetTransient(), instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator = null)
    {
        Add(typeof(TImplementation), ServiceScope.GetTransient(), instantiator);
        return this;
    }

    /// <inheritdoc/>
    public IServiceProvider Build()
    {
        return new ServiceProvider(_registeredTypes);
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder Configure(IServiceProviderBuilderConfiguration configuration)
    {
        return configuration.ConfigureBuilder(this);
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder ConfigureFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ConfigureFromAssembly(assembly);
        }

        return this;
    }

    /// <inheritdoc/>
    public IServiceProviderBuilder ConfigureFromAssembly(Assembly assembly)
    {
        var configurationTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(iface => iface.Name.EndsWith(nameof(IServiceProviderBuilderConfiguration))));
        
        foreach (var configurationType in configurationTypes)
        {
            var configurationInstance = Activator.CreateInstance(configurationType);
            if (configurationInstance is IServiceProviderBuilderConfiguration configuration)
            {
                Configure(configuration);
            }
        }

        return this;
    }

    /// <inheritdoc/>
    public void Reset()
    {
        _registeredTypes.Clear();
    }
    #endregion

    #region Private methods
    private void Add<TContract>(Type implementationType, IServiceScope scope, Func<TContract>? instantiator = null)
    {
        if (implementationType.IsInterface || implementationType.IsAbstract)
        {
            throw new ServiceTypeNotInstantiatableException(implementationType);
        }

        if (!implementationType.IsAssignableTo(typeof(TContract)))
        {
            throw new ServiceTypeNotAssignableException(typeof(TContract), implementationType);
        }
        
        if (typeof(TContract).IsGenericType && implementationType.IsGenericTypeDefinition)
        {
            implementationType = implementationType.MakeGenericType(typeof(TContract).GenericTypeArguments);
        }

        _registeredTypes.Remove(typeof(TContract));
        RegisteredType registerable = instantiator is null
            ? new RegisteredType(implementationType, scope)
            : new RegisteredType<TContract>(implementationType, scope, instantiator);
        _registeredTypes.Add(typeof(TContract), registerable);
    }
    #endregion
}