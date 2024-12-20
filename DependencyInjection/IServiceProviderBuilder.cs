namespace FolkerD0C.DependencyInjection;

public interface IServiceProviderBuilder
{
    public IServiceProviderBuilder AddScoped<TImplementation>(IExpirationScope scope, Func<TImplementation>? instantiator);

    public IServiceProviderBuilder AddScoped<TContract, TImplementation>(IExpirationScope scope, Func<TContract>? instantiator);

    public IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator);

    public IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator);

    public IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator);

    public IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator);

    public IServiceProvider Build();
}