using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Tests.Helpers;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderShould : TestBase
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
        var sut = _builder.AddScoped<IGuidGetterService, TestService>(new TimedScope(100)).Build();

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
            .AddScoped<GetterService<int>>(new TimedScope(30000))
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
    public void ThrowWhenResolvingUnregistered()
    {
        var sut = _builder.Build();
        var unregisteredAssertion = sut.Invoking(subject => subject.Resolve<int>())
            .Should()
            .Throw<ServiceTypeNotRegisteredException>()
            .WithMessage("* is not a registered type.");
        var exceptionFromUnregistered = unregisteredAssertion.Subject.First();
        exceptionFromUnregistered.ServiceType.Should().Be(typeof(int));
    }

    [Fact]
    public void GetServiceAndReturnTrue()
    {
        var expectedGuid = Guid.NewGuid();

        var sut = _builder
            .AddSingleton(() => new GetterService<Guid>(expectedGuid))
            .Build();

        var serviceIsResolved = sut.GetService(out GetterService<Guid>? actualService);

        serviceIsResolved.Should().BeTrue();
        actualService.Should().NotBeNull();
        actualService?.GetValue().Should().Be(expectedGuid);
    }

    [Fact]
    public void NotThrowExceptionIfItIsFromDI()
    {
        var sut = _builder.Build();

        var serviceIsResolved = sut.GetService(out Guid? service);

        serviceIsResolved.Should().BeFalse();
        service.Should().BeNull();
    }

    [Fact]
    public void ThrowExceptionIfItIsNotFromDI()
    {
        var fixture = new Fixture();
        var expectedMessage = fixture.Create<string>();

        var sut = _builder
            .AddSingleton<string>(() => 
            {
                throw new ApplicationException(expectedMessage);
            })
            .Build();
        
        var getServiceAssertion = sut.Invoking(subject => subject.GetService(out string? _))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void ThrowIfDefaultIsRequestedButHasNotBeenBuiltYet()
    {
        ResetGlobalState();
        Action gettingDefaultInstance = () =>
        {
            var instance = ServiceProvider.DefaultProvider;
        };

        gettingDefaultInstance
            .Should()
            .Throw<NullReferenceException>();
    }

    [Fact]
    public void NotThrowWhenBuildDefaultGetsCalledMultipleTimes()
    {
        ServiceProvider.BuildDefaultProvider();
        ServiceProvider.BuildDefaultProvider();
    }

    [Fact]
    public void ReturnSameDefaultEveryTime()
    {
        ResetGlobalState();
        ServiceProvider.BuildDefaultProvider();

        var firstCall = ServiceProvider.DefaultProvider;
        var secondCall = ServiceProvider.DefaultProvider;

        secondCall.Should().Be(firstCall);
    }

    [Fact]
    public void HaveTheSameServicesAsTheDefaultBuilderWhenDefault()
    {
        ResetGlobalState();
        var expectedServiceResponse = Guid.NewGuid();
        ServiceProviderBuilder.DefaultBuilder.AddSingleton(() =>
            new GetterService<Guid>(expectedServiceResponse));
        ServiceProvider.BuildDefaultProvider();

        ServiceProvider.DefaultProvider
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(expectedServiceResponse);
    }

    [Fact]
    public void BeEmptyWhenReset()
    {
        int excpectedServiceCount = 0;
        var sut = _builder
            .AddSingleton(() => new GetterService<Guid>(Guid.NewGuid()))
            .Build();

        sut.Reset();

        sut.GetRegisteredServiceTypes()
            .Count()
            .Should()
            .Be(excpectedServiceCount);
    }
}