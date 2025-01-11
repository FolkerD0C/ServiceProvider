using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.ConfigurationAssembly.One;

public class GuidConfiguration : IServiceProviderBuilderConfiguration
{
    public IServiceProviderBuilder Configure(IServiceProviderBuilder builder)
    {
        return builder.AddSingleton(() =>
            new GetterService<Guid>(ServiceResponses.GuidResponse));
    }
}
