namespace FolkerD0C.DependencyInjection.Exceptions;

/// <summary>
/// Represents the base exception type for errors related to dependency injection.
/// </summary>
public abstract class DependencyInjectionException : Exception
{
    private readonly Type _serviceType;

    /// <summary>
    /// Gets the type of the service that caused the exception.
    /// </summary>
    public Type ServiceType => _serviceType;

    internal DependencyInjectionException(Type serviceType, string message) : base(message)
    {
        _serviceType = serviceType;
    }
}

/// <summary>
/// Represents an exception thrown when a service type is not assignable to another type.
/// </summary>
public sealed class ServiceTypeNotAssignableException : DependencyInjectionException
{
    private readonly Type _assigneeType;

    /// <summary>
    /// Gets the type that was attempted to be assigned to the service type.
    /// </summary>
    public Type AssigneeType => _assigneeType;

    internal ServiceTypeNotAssignableException(Type serviceType, Type assigneeType) : base(serviceType, $"{assigneeType} is not assignable to {serviceType}.")
    {
        _assigneeType = assigneeType;
    }
}


/// <summary>
/// Represents an exception thrown when an object cannot be cast to a service type.
/// </summary>
public sealed  class ServiceTypeNotCastableException : DependencyInjectionException
{
    private readonly Type _originalType;

    /// <summary>
    /// Gets the original type that was attempted to be cast.
    /// </summary>
    public Type OriginalType => _originalType;

    internal ServiceTypeNotCastableException(Type serviceType, Type originalType) : base(serviceType, $"{originalType} type cannot be cast to {serviceType}.")
    {
        _originalType = originalType;
    }
}

/// <summary>
/// Represents an exception thrown when a service type cannot be instantiated.
/// </summary>
public sealed class ServiceTypeNotInstantiatableException : DependencyInjectionException
{
    internal ServiceTypeNotInstantiatableException(Type serviceType) : base(serviceType, $"{serviceType} is not an instantiatable type.")
    {
    }
}

/// <summary>
/// Represents an exception thrown when a service type is not registered in the dependency injection container.
/// </summary>
public sealed class ServiceTypeNotRegisteredException : DependencyInjectionException
{
    internal ServiceTypeNotRegisteredException(Type serviceType) : base(serviceType, $"{serviceType} is not a registered type.")
    {
    }
}

/// <summary>
/// Represents the base exception type for errors related to the global state of dependency injection.
/// </summary>
public abstract class DependencyInjectionGlobalStateException : Exception
{
    public DependencyInjectionGlobalStateException(string message) : base(message)
    {
    }
}

public sealed class InvalidServiceProviderKeyException : DependencyInjectionGlobalStateException
{
    private readonly object? _invalidKey;

    public object? InvalidKey => _invalidKey;

    internal InvalidServiceProviderKeyException(object? invalidKey) : base($"Invalid service provider key: '{invalidKey}'.")
    {
        _invalidKey = invalidKey;
    }
}

public sealed class ServiceProviderAlreadyExistsException : DependencyInjectionGlobalStateException
{
    private readonly object _serviceProviderKey;

    public object ServiceProviderKey => _serviceProviderKey;

    internal ServiceProviderAlreadyExistsException(object serviceProviderKey) : base($"Service provider with key '{serviceProviderKey}' already exists.")
    {
        _serviceProviderKey = serviceProviderKey;
    }
}

public sealed class ServiceProviderNotFoundException : DependencyInjectionGlobalStateException
{
    private readonly object _serviceProviderKey;

    public object ServiceProviderKey => _serviceProviderKey;

    internal ServiceProviderNotFoundException(object serviceProviderKey) : base($"Service provider with key '{serviceProviderKey}' was not found.")
    {
        _serviceProviderKey = serviceProviderKey;
    }
}

public class ServiceProvidersHaveBeenBuiltException : DependencyInjectionGlobalStateException
{
    internal ServiceProvidersHaveBeenBuiltException() : base("Service providers collections can only be built once.")
    {
    }
}
