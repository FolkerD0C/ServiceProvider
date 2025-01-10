using System.Reflection;
using FolkerD0C.DependencyInjection.Collections;

namespace FolkerD0C.DependencyInjection.Tests;

public abstract class TestBase
{
    protected void ResetGlobalState()
    {
        typeof(ServiceProvider)
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            .FirstOrDefault(fi => fi.Name.Equals("s_defaultProvider"))?
            .SetValue(null, null);
        ServiceProviderBuilder.DefaultBuilder.Reset();
        ServiceProviderCollection.DefaultProviderCollection.Reset();
        ServiceProviderBuilderCollection.DefaultBuilderCollection.Reset();
        typeof(ServiceProviderBuilderCollection)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(fi => fi.Name.Equals("_providerCollection"))?
            .SetValue(ServiceProviderBuilderCollection.DefaultBuilderCollection,
                ServiceProviderCollection.DefaultProviderCollection);
    }
}