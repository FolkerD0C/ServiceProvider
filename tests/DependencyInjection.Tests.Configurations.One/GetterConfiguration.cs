using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.AssemblyTests.One;

public class GetterConfiguration<TValue>(TValue gettableValue) : IServiceProviderBuilderConfiguration
{
    private readonly TValue _gettableValue = gettableValue;

    public IServiceProviderBuilder ConfigureBuilder(IServiceProviderBuilder builder)
    {
        return builder.AddSingleton(() => new GetterService<TValue>(_gettableValue));
    }
}
