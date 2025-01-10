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
        provider.GetRegisteredServiceTypes().Count().Should().Be(expectedRegisteredServiceCount);
    }

    [Fact]
    public void ThrowWhenServiceIsUnconstructable()
    {
        var abstractAssertion = _sut.Invoking(sut => sut.AddSingleton<UnConstructableService>())
            .Should()
            .Throw<ServiceTypeNotInstantiatableException>()
            .WithMessage("* is not an instantiatable type.");

        var exceptionFromAbstract = abstractAssertion.Subject.First();
        exceptionFromAbstract.ServiceType.Should().Be(typeof(UnConstructableService));
        
        var interfaceAssertion = _sut.Invoking(sut => sut.AddSingleton<IUnconstructableService>())
            .Should()
            .Throw<ServiceTypeNotInstantiatableException>()
            .WithMessage("* is not an instantiatable type.");
        var exceptionFromInterface = interfaceAssertion.Subject.First();
        exceptionFromInterface.ServiceType.Should().Be(typeof(IUnconstructableService));
    }

    [Fact]
    public void ThrowWhenImplementationIsUnassignableToContract()
    {
        var unassignableAssertion = _sut.Invoking(sut => sut.AddTransient<Guid, TestService>())
            .Should()
            .Throw<ServiceTypeNotAssignableException>()
            .WithMessage("* is not assignable to *");

        var exceptionFromUnassignable = unassignableAssertion.Subject.First();
        exceptionFromUnassignable.ServiceType.Should().Be(typeof(Guid));
        exceptionFromUnassignable.AssigneeType.Should().Be(typeof(TestService));
    }

    [Fact]
    public void DefaultInstanceIsEmpty()
    {
        int expectedRegisteredTypeCount = 0;
        var sut = ServiceProviderBuilder.DefaultInstance;

        sut.Build().GetRegisteredServiceTypes().Count().Should().Be(expectedRegisteredTypeCount);
    }
}
