using FolkerD0C.DependencyInjection.Collections;

namespace FolkerD0C.DependencyInjection.Configuration;

public interface IServiceProviderBuilderCollectionConfiguration
{
    IServiceProviderBuilderCollection ConfigureCollection(IServiceProviderBuilderCollection builderCollection);
}
