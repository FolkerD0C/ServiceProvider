namespace FolkerD0C.DependencyInjection.Collections;

public interface IServiceProviderCollection
{
    void AddServiceProvider(object key, IServiceProvider serviceProvider);

    IServiceProvider GetServiceProvider(object key);
}
