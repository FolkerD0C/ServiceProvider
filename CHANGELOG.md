# Changelog

<a name="1.0.2"></a>
## [1.0.2](https://www.github.com/FolkerD0C/ServiceProvider/releases/tag/v1.0.2) (2025-01-17)

### Bug Fixes

* Remove the IResettable interface and fix global state tests breaking ([fef11d0](https://www.github.com/FolkerD0C/ServiceProvider/commit/fef11d01828bfe6583cf89a077913415d9b3abbd))

<a name="1.0.1"></a>
## [1.0.1](https://www.github.com/FolkerD0C/ServiceProvider/releases/tag/v1.0.1) (2025-01-12)

### Bug Fixes

* Move method to where it logically belongs ([b36bd2b](https://www.github.com/FolkerD0C/ServiceProvider/commit/b36bd2b216add880c74f0332e1ff324111ecdb42))

<a name="1.0.0"></a>
## [1.0.0](https://www.github.com/FolkerD0C/ServiceProvider/releases/tag/v1.0.0) (2025-01-12)

Initial release

### Overview

The FolkerD0C.DependencyInjection package is a lightweight and extensible dependency injection framework for .NET applications, offering robust tools for managing services and configurations.

### Main features

- `IServiceProvider`
    - Core interface for resolving and managing services.
    - Features:
        - `Resolve<TContract>()` to resolve services.
        - `GetService<TContract>()` to try retrieving services.
        - `GetRegisteredServiceTypes()` for listing registered types.

- `IServiceProviderBuilder`
    - Fluent interface for configuring and building service providers.
    - Features:
        - Support for `AddScoped`, `AddSingleton`, and `AddTransient` services.
        - Configuration from assemblies.
        - `Build()` to create an `IServiceProvider`.

- `IServiceProviderCollection`
    - Interface for managing multiple service providers.
    - Features:
        - Add, retrieve, and list service providers.
        - Support for keyed service provider access.

- `IServiceProviderBuilderCollection`
    - Interface for managing collections of service provider builders.
    - Features:
        - Configure builders from assemblies.
        - Access individual builders by key.
        - `BuildAll()` to construct all providers in the collection.

