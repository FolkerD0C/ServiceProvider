namespace FolkerD0C.DependencyInjection.Configuration;

/// <summary>
/// Defines the configuration for an <see cref="IServiceProviderBuilder"/>.
/// </summary>
public interface IServiceProviderBuilderConfiguration
{
    /// <summary>
    /// Configures the specified <see cref="IServiceProviderBuilder"/> using the provided configuration logic.
    /// </summary>
    /// <param name="builder">The <see cref="IServiceProviderBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="IServiceProviderBuilder"/>.</returns>
    IServiceProviderBuilder ConfigureBuilder(IServiceProviderBuilder builder);
}