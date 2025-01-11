using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.ConfigurationAssemblies.One;

public class GuidPartialCollectionConfigurationOne : IServiceProviderBuilderCollectionConfiguration
{
    public IServiceProviderBuilderCollection ConfigureCollection(IServiceProviderBuilderCollection builderCollection)
    {
        builderCollection.GetBuilder(ServiceProviderKeys.ServiceProviderKeyOne)
            .AddSingleton(() => new GetterService<Guid>(ServiceProviderKeys.ServiceProviderKeyOne));
        return builderCollection;
    }
}
