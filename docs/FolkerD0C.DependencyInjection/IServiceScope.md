# IServiceScope interface

Defines a contract for objects that have an expiration scope. If a scope is expired then the object is recreated. If a scope is renewable then it gets renewed after expiration.

```csharp
public interface IServiceScope
```

## Members

| name | description |
| --- | --- |
| [IsRenewable](IServiceScope/IsRenewable.md) { get; } | Gets a value indicating whether the scope can be renewed. |
| [IsValidCurrently](IServiceScope/IsValidCurrently.md) { get; } | Gets a value indicating whether the scope is currently valid. |
| [Renew](IServiceScope/Renew.md)() | Renews the scope, resetting or extending its validity period. |

## See Also

* namespace [FolkerD0C.DependencyInjection](../FolkerD0C.DependencyInjection.md)

<!-- DO NOT EDIT: generated by xmldocmd for FolkerD0C.DependencyInjection.dll -->
