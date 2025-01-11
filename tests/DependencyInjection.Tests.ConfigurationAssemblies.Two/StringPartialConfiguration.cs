using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.ConfigurationAssemblies.Two;

public class StringPartialConfiguration : IServiceProviderBuilderConfiguration
{
    public IServiceProviderBuilder ConfigureBuilder(IServiceProviderBuilder builder)
    {
        return builder.AddSingleton(() =>
            new GetterService<string>(ServiceResponses.StringResponse));
    }
}
