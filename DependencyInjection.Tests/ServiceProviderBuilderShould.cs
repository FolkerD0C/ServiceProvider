using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Tests.Services;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderBuilderShould
{
    readonly IServiceProviderBuilder _sut;
    readonly Fixture _fixture;

    public ServiceProviderBuilderShould()
    {
        _sut = new ServiceProviderBuilder();
        _fixture = new();
    }

    [Fact]
    public void RegisterService()
    {
        string expectedService = _fixture.Create<string>();

        var provider = _sut
            .AddSingleton(() => expectedService)
            .Build();
        var actualService = provider.Resolve<string>();

        actualService.Should().Be(expectedService);
    }

    [Fact]
    public void OverwriteRegisteredService()
    {
        string originalService = _fixture.Create<string>();
        string expectedService = _fixture.Create<string>();

        var provider = _sut
            .AddSingleton(() => originalService)
            .AddSingleton(() => expectedService)
            .Build();
        var actualService = provider.Resolve<string>();

        actualService.Should().Be(expectedService);
        actualService.Should().NotBe(originalService);
    }

    [Fact]
    public void CreateProviderWithAllRegisteredServices()
    {
        string expectedStringService = _fixture.Create<string>();
        int expectedIntService = _fixture.Create<int>();
        IGuidGetterService expectedTestService = new TestService();
        int expectedRegisteredServiceCount = 3;

        var provider = _sut
            .AddSingleton(() => expectedStringService)
            .AddSingleton(() => expectedIntService)
            .AddTransient<IGuidGetterService, TestService>(() => expectedTestService)
            .Build();
        var actualStringService = provider.Resolve<string>();
        var actualIntService = provider.Resolve<int>();
        var actualGuidService = provider.Resolve<IGuidGetterService>();

        actualStringService.Should().Be(expectedStringService);
        actualIntService.Should().Be(expectedIntService);
        actualGuidService.GetGuid().Should().Be(expectedTestService.GetGuid());
        provider.GetRegisteredServices().Count().Should().Be(expectedRegisteredServiceCount);
    }

    [Fact]
    public void ThrowWhenServiceIsUnconstructable()
    {
        _sut.Invoking(sut => sut.AddSingleton<UnConstructableService>())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("* is not constructable.");
        _sut.Invoking(sut => sut.AddSingleton<IUnconstructableService>())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("* is not constructable.");
    }

    [Fact]
    public void ThrowWhenImplementationIsUnassignableToContract()
    {
        _sut.Invoking(sut => sut.AddTransient<Guid, TestService>())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("* is not assignable to *");
    }

    [Fact]
    public void DefaultInstanceIsEmpty()
    {
        int expectedRegisteredTypeCount = 0;
        var sut = ServiceProviderBuilder.DefaultInstance;

        sut.Build().GetRegisteredServices().Count().Should().Be(expectedRegisteredTypeCount);
    }
}
