namespace FolkerD0C.DependencyInjection.Collections;

public interface IServiceProviderBuilderCollection
{
    public IServiceProviderCollection BuildAll();

    public IServiceProviderBuilder GetBuilder(object key);
}
