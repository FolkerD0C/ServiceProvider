# IServiceProviderBuilderCollection.BuildAll method

Builds and returns a collection of service providers from the builders in the collection. After building the providers it disposes the builder collection.

```csharp
public IServiceProviderCollection BuildAll()
```

## Return Value

An [`IServiceProviderCollection`](../IServiceProviderCollection.md) containing all service providers built from the builders in the collection.

## Exceptions

| exception | condition |
| --- | --- |
| [ServiceProvidersHaveBeenBuiltException](../../FolkerD0C.DependencyInjection.Exceptions/ServiceProvidersHaveBeenBuiltException.md) | Thrown if this method has already been called. |

## See Also

* interface [IServiceProviderCollection](../IServiceProviderCollection.md)
* interface [IServiceProviderBuilderCollection](../IServiceProviderBuilderCollection.md)
* namespace [FolkerD0C.DependencyInjection.Collections](../../FolkerD0C.DependencyInjection.md)

<!-- DO NOT EDIT: generated by xmldocmd for FolkerD0C.DependencyInjection.dll -->
