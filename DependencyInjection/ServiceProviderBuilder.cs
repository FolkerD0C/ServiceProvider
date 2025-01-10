namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Provides an implementation for the <see cref="IServiceProviderBuilder"/> interface.
/// </summary>
public sealed class ServiceProviderBuilder : IServiceProviderBuilder
{
    public static readonly IServiceProviderBuilder DefaultInstance = new ServiceProviderBuilder();

    private readonly Dictionary<Type, RegisteredType> _registeredTypes = new();

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