using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Tests.Services;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderShould
{
    readonly IServiceProviderBuilder _builder;

    public ServiceProviderShould()
    {
        _builder = new ServiceProviderBuilder();
    }

    [Fact]
    public void ProvideDifferentServicesWhenTransient()
    {
        var sut =_builder.AddTransient<IGuidGetterService, TestService>().Build();

        var firstTransientGuid = sut.Resolve<IGuidGetterService>().GetGuid();
        var secondTransientGuid = sut.Resolve<IGuidGetterService>().GetGuid();

        secondTransientGuid.Should().NotBe(firstTransientGuid);
    }

    [Fact]
    public void RenewServiceWhenExpired()
    {
        var sut = _builder.AddScoped<IGuidGetterService, TestService>(new TimedExpirationScope(100)).Build();

        var firstScopedGuid = sut.Resolve<IGuidGetterService>().GetGuid();
        var secondScopedGuid = sut.Resolve<IGuidGetterService>().GetGuid();
        Thread.Sleep(150);
        var thirdScopedGuid = sut.Resolve<IGuidGetterService>().GetGuid();

        secondScopedGuid.Should().Be(firstScopedGuid);
        thirdScopedGuid.Should().NotBe(secondScopedGuid);
    }

    [Fact]
    public void ResolveHierarchy()
    {
        IGuidGetterService expectedGuidService = new TestService();
        IStringGetterService expectedStringService = new TestService();

        var sut = _builder
            .AddSingleton<IGuidGetterService, TestService>(() => expectedGuidService)
            .AddSingleton<IStringGetterService, TestService>(() => expectedStringService)
            .AddSingleton<IMiddleManService, MiddleManService>()
            .AddSingleton<IOtherMiddleManService, OtherMiddleManService>()
            .AddSingleton<ITopLevelService, TopLevelService>()
            .Build();
        
        var service = sut.Resolve<ITopLevelService>();

        service
            .GetMiddleManService()
            .GetGuidGetterService()
            .GetGuid()
            .Should()
            .Be(expectedGuidService.GetGuid());
        service
            .GetOtherMiddleManService()
            .GetStringGetterService()
            .GetString()
            .Should()
            .Be(expectedStringService.GetString());
        service.Should().BeOfType<TopLevelService>();
    }

    [Fact]
    public void ResolveGenerics()
    {
        var fixture = new Fixture();
        var expectedInt = fixture.Create<int>();
        var expectedString = fixture.Create<string>();

        var sut = _builder
            .AddTransient(() => expectedInt)
            .AddTransient(() => expectedString)
            .AddScoped<GetterService<int>>(new TimedExpirationScope(30000))
            .AddSingleton<GetterService<string>>()
            .AddTransient<IOtherGenericService<int, string>, GenericService<int, string>>()
            .Build();
        var service = sut.Resolve<IOtherGenericService<int, string>>();

        service.GetT().Should().Be(expectedInt);
        service.GetU().Should().Be(expectedString);
        service.Should().BeOfType<GenericService<int, string>>();
    }

    [Fact]
    public void ResolveSpecifiedGenerics()
    {
        var expectedGuid = Guid.NewGuid();

        var sut = _builder
            .AddSingleton(() => new GetterService<Guid>(expectedGuid))
            .AddSingleton<IGenericService<Guid>, SpecificGenericService>()
            .Build();
        var service = sut.Resolve<IGenericService<Guid>>();

        service.GetT().Should().Be(expectedGuid);
        service.Should().BeOfType<SpecificGenericService>();
    }

    [Fact]
    public void ThrowIfUnregisteredResolutionIsNeeded()
    {
        var sut = _builder.Build();
        sut.Invoking(subject => subject.Resolve<int>())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("* is not a registered type.");
    }
}