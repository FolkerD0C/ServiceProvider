
namespace FolkerD0C.DependencyInjection;

public class ServiceProviderBuilder : IServiceProviderBuilder
{
    #region Public methods
    public IServiceProviderBuilder AddScoped<TImplementation>(IExpirationScope scope, Func<TImplementation>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProviderBuilder AddScoped<TContract, TImplementation>(IExpirationScope scope, Func<TContract>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator = null)
    {
        throw new NotImplementedException();
    }

    public IServiceProvider Build()
    {
        throw new NotImplementedException();
    }
    #endregion
}