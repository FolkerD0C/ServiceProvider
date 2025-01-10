namespace FolkerD0C.DependencyInjection;

internal record RegisteredType (Type ImplementationType, IServiceScope Scope);

internal record RegisteredType<TContract>(
    Type ImplementationType, IServiceScope Scope, Func<TContract>? Instantiator)
    : RegisteredType(ImplementationType, Scope);