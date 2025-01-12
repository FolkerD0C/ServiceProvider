# FolkerD0C.DependencyInjection assembly

## FolkerD0C.DependencyInjection namespace

| public type | description |
| --- | --- |
| interface [IResettable](./FolkerD0C.DependencyInjection/IResettable.md) | Defines an interface for objects that can be reset to their initial state. |
| interface [IServiceProvider](./FolkerD0C.DependencyInjection/IServiceProvider.md) | Provides services that are registered in an [`IServiceProviderBuilder`](./FolkerD0C.DependencyInjection/IServiceProviderBuilder.md). |
| interface [IServiceProviderBuilder](./FolkerD0C.DependencyInjection/IServiceProviderBuilder.md) | Can build a service provider with support for scoped, singleton, and transient lifetimes. |
| interface [IServiceScope](./FolkerD0C.DependencyInjection/IServiceScope.md) | Defines a contract for objects that have an expiration scope. If a scope is expired then the object is recreated. If a scope is renewable then it gets renewed after expiration. |
| class [ServiceProvider](./FolkerD0C.DependencyInjection/ServiceProvider.md) |  |
| class [ServiceProviderBuilder](./FolkerD0C.DependencyInjection/ServiceProviderBuilder.md) |  |

## FolkerD0C.DependencyInjection.Collections namespace

| public type | description |
| --- | --- |
| interface [IServiceProviderBuilderCollection](./FolkerD0C.DependencyInjection.Collections/IServiceProviderBuilderCollection.md) | Represents a collection of service provider builders that can construct a collection of service providers. |
| interface [IServiceProviderCollection](./FolkerD0C.DependencyInjection.Collections/IServiceProviderCollection.md) | Represents a collection of service providers identified by unique keys. |
| class [ServiceProviderBuilderCollection](./FolkerD0C.DependencyInjection.Collections/ServiceProviderBuilderCollection.md) |  |
| class [ServiceProviderCollection](./FolkerD0C.DependencyInjection.Collections/ServiceProviderCollection.md) | Can only be created through [`BuildAll`](./FolkerD0C.DependencyInjection.Collections/IServiceProviderBuilderCollection/BuildAll.md). |

## FolkerD0C.DependencyInjection.Configuration namespace

| public type | description |
| --- | --- |
| interface [IServiceProviderBuilderCollectionConfiguration](./FolkerD0C.DependencyInjection.Configuration/IServiceProviderBuilderCollectionConfiguration.md) | Defines the configuration for an [`IServiceProviderBuilderCollection`](./FolkerD0C.DependencyInjection.Collections/IServiceProviderBuilderCollection.md). |
| interface [IServiceProviderBuilderConfiguration](./FolkerD0C.DependencyInjection.Configuration/IServiceProviderBuilderConfiguration.md) | Defines the configuration for an [`IServiceProviderBuilder`](./FolkerD0C.DependencyInjection/IServiceProviderBuilder.md). |

## FolkerD0C.DependencyInjection.Exceptions namespace

| public type | description |
| --- | --- |
| abstract class [DependencyInjectionBaseException](./FolkerD0C.DependencyInjection.Exceptions/DependencyInjectionBaseException.md) | Represents the base exception type for all exceptions inside this package. |
| abstract class [DependencyInjectionCollectionException](./FolkerD0C.DependencyInjection.Exceptions/DependencyInjectionCollectionException.md) | Represents the base exception type for errors related to dependency injection collections, specifically [`IServiceProviderCollection`](./FolkerD0C.DependencyInjection.Collections/IServiceProviderCollection.md) and [`IServiceProviderBuilderCollection`](./FolkerD0C.DependencyInjection.Collections/IServiceProviderBuilderCollection.md). |
| abstract class [DependencyInjectionException](./FolkerD0C.DependencyInjection.Exceptions/DependencyInjectionException.md) | Represents the base exception type for errors related to [`IServiceProvider`](./FolkerD0C.DependencyInjection/IServiceProvider.md) and [`IServiceProviderBuilder`](./FolkerD0C.DependencyInjection/IServiceProviderBuilder.md). |
| class [InvalidServiceProviderException](./FolkerD0C.DependencyInjection.Exceptions/InvalidServiceProviderException.md) | Represents an exception that is thrown when an invalid service provider is encountered. |
| class [InvalidServiceProviderKeyException](./FolkerD0C.DependencyInjection.Exceptions/InvalidServiceProviderKeyException.md) | Represents an exception that is thrown when an invalid service provider key is encountered. |
| class [ServiceProviderAlreadyExistsException](./FolkerD0C.DependencyInjection.Exceptions/ServiceProviderAlreadyExistsException.md) | Represents an exception that is thrown when a service provider with a specified key already exists in the collection. |
| class [ServiceProviderNotFoundException](./FolkerD0C.DependencyInjection.Exceptions/ServiceProviderNotFoundException.md) | Represents an exception that is thrown when a service provider with a specified key is not found in the collection. |
| class [ServiceProvidersHaveBeenBuiltException](./FolkerD0C.DependencyInjection.Exceptions/ServiceProvidersHaveBeenBuiltException.md) | Represents an exception that is thrown when an attempt is made to rebuild a service provider collection after it has already been built. |
| class [ServiceTypeNotAssignableException](./FolkerD0C.DependencyInjection.Exceptions/ServiceTypeNotAssignableException.md) | Represents an exception thrown when a service type is not assignable to another type. |
| class [ServiceTypeNotCastableException](./FolkerD0C.DependencyInjection.Exceptions/ServiceTypeNotCastableException.md) | Represents an exception thrown when an object cannot be cast to a service type. |
| class [ServiceTypeNotInstantiatableException](./FolkerD0C.DependencyInjection.Exceptions/ServiceTypeNotInstantiatableException.md) | Represents an exception thrown when a service type cannot be instantiated. |
| class [ServiceTypeNotRegisteredException](./FolkerD0C.DependencyInjection.Exceptions/ServiceTypeNotRegisteredException.md) | Represents an exception thrown when a service type is not registered in the dependency injection container. |

<!-- DO NOT EDIT: generated by xmldocmd for FolkerD0C.DependencyInjection.dll -->
