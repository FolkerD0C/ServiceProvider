using System.Reflection;

namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Provides an implementation for the <see cref="IServiceProvider"/> interface.
/// </summary>
public class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, RegisteredType> _registeredTypes;
    private readonly Dictionary<Type, object?> _cachedObjects = [];
    private MethodInfo? _resolveMethod;

    internal ServiceProvider(Dictionary<Type, RegisteredType> registeredTypes)
    {
        _registeredTypes = registeredTypes;
    }

    #region Public methods
    /// <inheritdoc/>
    public IEnumerable<Type> GetRegisteredServices()
        => _registeredTypes.Select(kvp => kvp.Key);

    /// <inheritdoc/>
    public TContract Resolve<TContract>()
    {
        var registeredType = GetRegisteredType<TContract>();

        return InternalResolve<TContract>(registeredType);
    }
    #endregion

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
            if (registeredType is RegisteredType<TContract> parameterizedRegisteredType)
            {
                return parameterizedRegisteredType;
            }
            return registeredType;
        }
        throw new InvalidOperationException($"{typeof(TContract)} is not a registered type.");
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
