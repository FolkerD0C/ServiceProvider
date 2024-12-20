namespace FolkerD0C.DependencyInjection;

internal record RegisteredType (Type ImplementationType, IExpirationScope Scope);

internal record RegisteredType<TContract>(
    Type ImplementationType, IExpirationScope Scope, Func<TContract>? Instantiator)
    : RegisteredType(ImplementationType, Scope);