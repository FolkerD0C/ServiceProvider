# Public API

Here is the full public API of the project. This is not the documentation.

```csharp
namespace FolkerD0C.DependencyInjection
{
    public interface IResettable
    {
        void Reset();
    }
    public interface IServiceProvider : IResettable
    {
        IEnumerable<Type> GetRegisteredServiceTypes();
        TContract Resolve<TContract>();
        bool GetService<TContract>(out TContract? serviceInstance);
    }
    public sealed partial class ServiceProvider : IServiceProvider
    {
        public static readonly IServiceProvider DefaultProvider;
        public static void BuildDefaultProvider();
    }
    public interface IServiceProviderBuilder : IResettable
    {
        IServiceProviderBuilder AddScoped<TImplementation>(IServiceScope scope, Func<TImplementation>? instantiator = null);
        IServiceProviderBuilder AddScoped<TContract, TImplementation>(IServiceScope scope, Func<TContract>? instantiator = null);
        IServiceProviderBuilder AddSingleton<TImplementation>(Func<TImplementation>? instantiator = null);
        IServiceProviderBuilder AddSingleton<TContract, TImplementation>(Func<TContract>? instantiator = null);
        IServiceProviderBuilder AddTransient<TImplementation>(Func<TImplementation>? instantiator = null);
        IServiceProviderBuilder AddTransient<TContract, TImplementation>(Func<TContract>? instantiator = null);
        IServiceProvider Build();
        IServiceProviderBuilder Configure(IServiceProviderBuilderConfiguration builderConfiguration);
        IServiceProviderBuilder ConfigureFromAssembly(Assembly assembly);
        IServiceProviderBuilder ConfigureFromAssemblies(params Assembly[] assemblies);
    }
    public sealed partial class ServiceProviderBuilder : IServiceProviderBuilder
    {
        public static readonly IServiceProviderBuilder DefaultBuilder;
        public static IServiceProviderBuilder ConfigureDefault(IServiceProviderBuilderConfiguration configuration);
        public static IServiceProviderBuilder ConfigureDefaultFromAssembly(Assembly assembly);
        public static IServiceProviderBuilder ConfigureDefaultFromAssemblies(params Assembly[] assemblies);
    }
    public interface IServiceScope
    {
        bool IsRenewable { get; }
        bool IsValidCurrently { get; }
        void Renew();
    }
}
namespace FolkerD0C.DependencyInjection.Collections
{
    public interface IServiceProviderCollection : IResettable
    {
        void AddServiceProvider(object key, IServiceProvider serviceProvider);
        IServiceProvider GetServiceProvider(object key);
        IEnumerable<IServiceProvider> GetServiceProviders();
        bool TryAddServiceProvider(object key, IServiceProvider serviceProvider);
    }
    public sealed partial class ServiceProviderCollection : IServiceProviderCollection
    {
        public static readonly IServiceProviderCollection DefaultProviderCollection;
    }
    public interface IServiceProviderBuilderCollection : IResettable
    {
        IServiceProviderCollection BuildAll();
        IServiceProviderBuilderCollection Configure(IServiceProviderBuilderCollectionConfiguration configuration);
        IServiceProviderBuilderCollection ConfigureFromAssembly(Assembly assembly);
        IServiceProviderBuilderCollection ConfigureFromAssemblies(params Assembly[] assemblies);
        IServiceProviderBuilder GetBuilder(object key);
    }
    public sealed partial class ServiceProviderBuilderCollection : IServiceProviderBuilderCollection
    {
        public static readonly IServiceProviderBuilderCollection DefaultBuilderCollection;
        public static IServiceProviderBuilderCollection ConfigureDefault(IServiceProviderBuilderCollectionConfiguration configuration);
        public static IServiceProviderBuilderCollection ConfigureDefaultFromAssembly(Assembly assembly);
        public static IServiceProviderBuilderCollection ConfigureDefaultFromAssemblies(params Assembly[] assemblies);
    }
}
namespace FolkerD0C.DependencyInjection.Configuration
{
    public interface IServiceProviderBuilderConfiguration
    {
        IServiceProviderBuilder ConfigureBuilder(IServiceProviderBuilder builder);
    }
    public interface IServiceProviderBuilderCollectionConfiguration
    {
        IServiceProviderBuilderCollection ConfigureBuilderCollection(IServiceProviderBuilderCollection builderCollection);
    }
}

```