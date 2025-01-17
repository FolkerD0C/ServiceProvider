using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Utilities;
using System.Reflection;

namespace FolkerD0C.DependencyInjection;

/// <inheritdoc cref="IServiceProvider"/>
public sealed class ServiceProvider : IServiceProvider
{
    private static IServiceProvider? s_defaultProvider;
    private readonly Dictionary<Type, RegisteredType> _registeredTypes;
    private readonly Dictionary<Type, object?> _cachedObjects = [];
    private MethodInfo? _resolveMethod;

    /// <summary>
    /// Gets the default instance. Throws a <see cref="NullReferenceException"/> if
    /// it has not been built yet. Use the <see cref="ServiceProviderBuilder.BuildDefault"/>
    /// method to build the deafult instance.
    /// </summary>
    public static IServiceProvider DefaultProvider
    {
        get
        {
            if (s_defaultProvider is null)
            {
                throw new NullReferenceException("Default service provider has not been built yet.");
            }
            return s_defaultProvider;
        }
    }

    internal ServiceProvider(Dictionary<Type, RegisteredType> registeredTypes)
    {
        _registeredTypes = registeredTypes;
    }

    #region Public methods
    /// <inheritdoc/>
    public IEnumerable<Type> GetRegisteredServiceTypes()
        => _registeredTypes.Select(kvp => kvp.Key);
    
    /// <inheritdoc/>
    public bool GetService<TContract>(out TContract? serviceInstance)
    {
        serviceInstance = default;
        bool resolutionResult = false;
        try
        {
            serviceInstance = Resolve<TContract>();
            resolutionResult = true;
        }
        catch (DependencyInjectionException)
        {
        }
        catch (Exception)
        {
            throw;
        }
        return resolutionResult;
    }

    /// <inheritdoc/>
    public TContract Resolve<TContract>()
    {
        var registeredType = GetRegisteredType<TContract>();

        return InternalResolve<TContract>(registeredType);
    }

#if CAN_RESET_GLOBAL_STATE
    /// <summary>
    /// A method used in tests to reset static defaults
    /// </summary>
    public static void ResetGlobalState()
    {
        s_defaultProvider = null;
    }
#endif
    #endregion

    internal static void SetDefaultProvider(IServiceProvider serviceProvider)
    {
        s_defaultProvider ??= serviceProvider;
    }

    #region Private methods
    private TContract Create<TContract>(RegisteredType registeredType)
    {
        if (registeredType is RegisteredType<TContract> parameterizedRegisteredType
            && parameterizedRegisteredType.Instantiator is not null)
        {
            return parameterizedRegisteredType.Instantiator();
        }

        if (_resolveMethod == null)
        {
            _resolveMethod = GetType().GetMethod("Resolve");
        }

        var constructorParameters = registeredType.ImplementationType
            .GetConstructors().Concat(registeredType.ImplementationType.GetConstructors(BindingFlags.NonPublic))
            .Select(constructorInfo =>
                new
                {
                    Constructor = constructorInfo,
                    Parameters = constructorInfo.GetParameters()
                })
            .OrderByDescending(a => a.Parameters.Length)
            .First().Parameters
            .Select(parameterInfo =>
            {
                var genericResolveMethod = _resolveMethod?.MakeGenericMethod(parameterInfo.ParameterType);
                return genericResolveMethod?.Invoke(this, null);
            })
            .ToArray();

        object? result = Activator.CreateInstance(registeredType.ImplementationType, constructorParameters);

        return result.GuardedCast<TContract>();
    }

    private RegisteredType GetRegisteredType<TContract>()
    {
        if (_registeredTypes.TryGetValue(typeof(TContract), out RegisteredType? registeredType)
            || (typeof(TContract).IsGenericType
                && _registeredTypes.TryGetValue(typeof(TContract).GetGenericTypeDefinition(), out registeredType)))
        {
            return registeredType;
        }
        throw new ServiceTypeNotRegisteredException(typeof(TContract));
    }

    private TContract HandleExpiredScope<TContract>(RegisteredType registeredType)
    {
        _cachedObjects.Remove(typeof(TContract));

        TContract result = Create<TContract>(registeredType);

        if (registeredType.Scope.IsRenewable)
        {
            registeredType.Scope.Renew();
            if (registeredType.Scope.IsValidCurrently)
            {
                _cachedObjects.Add(typeof(TContract), result);
            }   
        }

        return result;
    }

    private TContract HandleValidScope<TContract>(RegisteredType registeredType)
    {
        if (_cachedObjects.ContainsKey(typeof(TContract)))
        {
            return _cachedObjects[typeof(TContract)].GuardedCast<TContract>();
        }

        TContract result = Create<TContract>(registeredType);
        _cachedObjects.Add(typeof(TContract), result);
        return result;
    }

    private TContract InternalResolve<TContract>(RegisteredType registeredType)
    {
        return registeredType.Scope.IsValidCurrently
            ? HandleValidScope<TContract>(registeredType)
            : HandleExpiredScope<TContract>(registeredType);
    }
    #endregion
}
