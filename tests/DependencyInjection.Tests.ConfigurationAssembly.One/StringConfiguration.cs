using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.ConfigurationAssembly.One;

public class StringConfiguration : IServiceProviderBuilderConfiguration
{
    public IServiceProviderBuilder ConfigureBuilder(IServiceProviderBuilder builder)
    {
        return builder.AddSingleton(() => new GetterService<string>(ServiceResponses.StringResponse));
    }
}
