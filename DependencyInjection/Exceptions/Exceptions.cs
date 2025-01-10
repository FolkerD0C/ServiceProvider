namespace FolkerD0C.DependencyInjection.Exceptions;

/// <summary>
/// Represents the base exception type for all exceptions inside this package.
/// </summary>
public abstract class DependencyInjectionBaseException(string message) : Exception(message)
{
}

/// <summary>
/// Represents the base exception type for errors related to
/// <see cref="IServiceProvider"/> and <see cref="IServiceProviderBuilder"/>.
/// </summary>
public abstract class DependencyInjectionException(Type serviceType, string message)
    : DependencyInjectionBaseException(message)
{
    private readonly Type _serviceType = serviceType;

    /// <summary>
    /// Gets the type of the service that caused the exception.
    /// </summary>
    public Type ServiceType => _serviceType;
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

    internal ServiceTypeNotAssignableException(Type serviceType, Type assigneeType)
        : base(serviceType, $"{assigneeType} is not assignable to {serviceType}.")
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

    internal ServiceTypeNotCastableException(Type serviceType, Type originalType)
        : base(serviceType, $"{originalType} type cannot be cast to {serviceType}.")
    {
        _originalType = originalType;
    }
}

/// <summary>
/// Represents an exception thrown when a service type cannot be instantiated.
/// </summary>
public sealed class ServiceTypeNotInstantiatableException : DependencyInjectionException
{
    internal ServiceTypeNotInstantiatableException(Type serviceType)
        : base(serviceType, $"{serviceType} is not an instantiatable type.")
    {
    }
}

/// <summary>
/// Represents an exception thrown when a service type is not registered in the dependency injection container.
/// </summary>
public sealed class ServiceTypeNotRegisteredException : DependencyInjectionException
{
    internal ServiceTypeNotRegisteredException(Type serviceType)
        : base(serviceType, $"{serviceType} is not a registered type.")
    {
    }
}

/// <summary>
/// Represents the base exception type for errors related to dependency injection collections,
/// specifically <see cref="Collections.IServiceProviderCollection"/>
/// and <see cref="Collections.IServiceProviderBuilderCollection"/>.
/// </summary>
public abstract class DependencyInjectionCollectionException(string message)
    : DependencyInjectionBaseException(message)
{
}

/// <summary>
/// Represents an exception that is thrown when an invalid service provider is encountered.
/// </summary>
public class InvalidServiceProviderException : DependencyInjectionCollectionException
{
    private readonly IServiceProvider? _serviceProvider;

    /// <summary>
    /// Gets the invalid service provider that caused the exception.
    /// </summary>
    public IServiceProvider? ServiceProvider => _serviceProvider;

    internal InvalidServiceProviderException(IServiceProvider? serviceProvider)
        : base($"Invalid service provider '{serviceProvider}")
    {
        _serviceProvider = serviceProvider;
    }
}

/// <summary>
/// Represents an exception that is thrown when an invalid service provider key is encountered.
/// </summary>
public sealed class InvalidServiceProviderKeyException : DependencyInjectionCollectionException
{
    private readonly object? _invalidKey;

    /// <summary>
    /// Gets the invalid key that caused the exception.
    /// </summary>
    public object? InvalidKey => _invalidKey;

    internal InvalidServiceProviderKeyException(object? invalidKey)
        : base($"Invalid service provider key: '{invalidKey}'.")
    {
        _invalidKey = invalidKey;
    }
}


/// <summary>
/// Represents an exception that is thrown when a service provider with a specified key already exists in the collection.
/// </summary>
public sealed class ServiceProviderAlreadyExistsException : DependencyInjectionCollectionException
{
    private readonly object _serviceProviderKey;

    /// <summary>
    /// Gets the key of the service provider that already exists.
    /// </summary>
    public object ServiceProviderKey => _serviceProviderKey;

    internal ServiceProviderAlreadyExistsException(object serviceProviderKey)
        : base($"Service provider with key '{serviceProviderKey}' already exists.")
    {
        _serviceProviderKey = serviceProviderKey;
    }
}

/// <summary>
/// Represents an exception that is thrown when a service
/// provider with a specified key is not found in the collection.
/// </summary>
public sealed class ServiceProviderNotFoundException : DependencyInjectionCollectionException
{
    private readonly object _serviceProviderKey;

    /// <summary>
    /// Gets the key of the service provider that was not found.
    /// </summary>
    public object ServiceProviderKey => _serviceProviderKey;

    internal ServiceProviderNotFoundException(object serviceProviderKey)
        : base($"Service provider with key '{serviceProviderKey}' was not found.")
    {
        _serviceProviderKey = serviceProviderKey;
    }
}

/// <summary>
/// Represents an exception that is thrown when an attempt is made to
/// rebuild a service provider collection after it has already been built.
/// </summary>
public class ServiceProvidersHaveBeenBuiltException : DependencyInjectionCollectionException
{
    internal ServiceProvidersHaveBeenBuiltException()
        : base("Service providers collections can only be built once.")
    {
    }
}
