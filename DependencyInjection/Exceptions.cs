namespace FolkerD0C.DependencyInjection;

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
public class ServiceTypeNotAssignableException : DependencyInjectionException
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
public class ServiceTypeNotCastableException : DependencyInjectionException
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
public class ServiceTypeNotInstantiatableException : DependencyInjectionException
{
    internal ServiceTypeNotInstantiatableException(Type serviceType) : base(serviceType, $"{serviceType} is not an instantiatable type.")
    {
    }
}

/// <summary>
/// Represents an exception thrown when a service type is not registered in the dependency injection container.
/// </summary>
public class ServiceTypeNotRegisteredException : DependencyInjectionException
{
    internal ServiceTypeNotRegisteredException(Type serviceType) : base(serviceType, $"{serviceType} is not a registered type.")
    {
    }
}
