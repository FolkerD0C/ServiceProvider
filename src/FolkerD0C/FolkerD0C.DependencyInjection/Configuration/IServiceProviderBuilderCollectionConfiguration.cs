using FolkerD0C.DependencyInjection.Collections;

namespace FolkerD0C.DependencyInjection.Configuration;

/// <summary>
/// Defines the configuration for an <see cref="IServiceProviderBuilderCollection"/>.
/// </summary>
public interface IServiceProviderBuilderCollectionConfiguration
{
    /// <summary>
    /// Configures the specified <see cref="IServiceProviderBuilderCollection"/> using the provided configuration logic.
    /// </summary>
    /// <param name="builderCollection">The <see cref="IServiceProviderBuilderCollection"/> to configure.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilderCollection"/>.</returns>
    IServiceProviderBuilderCollection ConfigureBuilderCollection(IServiceProviderBuilderCollection builderCollection);
}
