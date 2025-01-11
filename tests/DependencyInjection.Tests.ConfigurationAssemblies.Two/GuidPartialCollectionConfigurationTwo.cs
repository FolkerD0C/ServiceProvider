using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.ConfigurationAssemblies.Two;

public class GuidPartialCollectionConfigurationTwo : IServiceProviderBuilderCollectionConfiguration
{
    public IServiceProviderBuilderCollection ConfigureBuilderCollection(IServiceProviderBuilderCollection builderCollection)
    {
        builderCollection.GetBuilder(ServiceProviderKeys.ServiceProviderKeyTwo)
            .AddSingleton(() => new GetterService<Guid>(ServiceProviderKeys.ServiceProviderKeyTwo));
        return builderCollection;
    }
}
