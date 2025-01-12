# ServiceProvider

## Table of contents
- [Overview](#overview)
- [Features](#features)
- [Usage](#usage)
- [Documentation](#documentation)
- [Built with](#built-with)
- [Contributions](#contributions)
- [License](#license)

## Overview
The `FolkerD0C.DependencyInjection` library is a lightweight dependency injection framework designed to offer a robust API for building and managing service providers and their lifecycles. This framework enables developers to register, configure, and resolve services with support for scoped, singleton, and transient lifetimes, as well as advanced configuration options via assemblies.

## Features
- **Service Registration:** Support for transient, scoped, and singleton service lifetimes.
- **Service Resolution:** Resolve services using contracts and manage multiple service providers.
- **Builder Configurations:** Configure service providers dynamically using assemblies or custom logic.
- **Service Collection:** Manage multiple service providers in collections for advanced dependency injection scenarios.
- **Resettable Interfaces:** Easily reset providers and builders to initial states.

## Usage

### Basic Service Registration and Resolution
```csharp
using FolkerD0C.DependencyInjection;

// Create a service provider builder
var builder = new ServiceProviderBuilder();

// Register services
builder.AddSingleton<IMyService, MyService>();
builder.AddTransient<IAnotherService, AnotherService>();

// Build the service provider
var serviceProvider = builder.Build();

// Resolve a service
var myService = serviceProvider.Resolve<IMyService>();
```

### Scoped Services
```csharp
using FolkerD0C.DependencyInjection;

var scope = new CustomServiceScope(); // Implement IServiceScope
var builder = new ServiceProviderBuilder();

// Register scoped service
builder.AddScoped<IMyScopedService>(scope, () => new MyScopedService());
var serviceProvider = builder.Build();

// Resolve service within scope
var scopedService = serviceProvider.Resolve<IMyScopedService>();
```

### Using the Default Provider
```csharp
using FolkerD0C.DependencyInjection;

ServiceProvider.BuildDefaultProvider();
var defaultProvider = ServiceProvider.DefaultProvider;

// Use default provider to resolve services
var serviceInstance = defaultProvider.Resolve<IMyService>();
```

### Managing Multiple Service Providers
```csharp
using FolkerD0C.DependencyInjection.Collections;

var collection = new ServiceProviderCollection();
collection.AddServiceProvider("provider1", new ServiceProviderBuilder().Build());
collection.AddServiceProvider("provider2", new ServiceProviderBuilder().Build());

var provider1 = collection.GetServiceProvider("provider1");
```

## Documentation
Please visit the [official documentation](https://github.com/FolkerD0C/ServiceProvider/blob/master/docs/FolkerD0C.DependencyInjection.md) for this project.

## Built with
- [.NET](https://dotnet.microsoft.com/en-us/)
- [XmlDocMarkdown](https://github.com/ejball/XmlDocMarkdown)
- [xunit](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)

## Contributions
Contributions are welcome! Please submit issues or pull requests via the [GitHub repository](https://github.com/FolkerD0C/ServiceProvider).

## License
This project is licensed under the GNU General Public License v3.0. See the [LICENSE](LICENSE) file for details.

[(Back to top)](#table-of-contents)

