using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Configuration;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests.Configurations.One;

public class GetterCollectionConfiguration<TValue>(TValue gettableValue) : IServiceProviderBuilderCollectionConfiguration
{
    private readonly TValue _gettableValue = gettableValue;

    public IServiceProviderBuilderCollection ConfigureCollection(IServiceProviderBuilderCollection builderCollection)
    {
        builderCollection.GetBuilder(_gettableValue ?? throw new NotImplementedException(
            $"{GetType()} did not have a value to inject."))
            .AddSingleton(() => new GetterService<TValue>(_gettableValue));
        return builderCollection;
    }
}
