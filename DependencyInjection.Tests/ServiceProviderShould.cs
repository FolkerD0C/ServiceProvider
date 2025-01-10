using System.Reflection;
using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Exceptions;
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
        GlobalStateResetter();
        Action gettingDefaultInstance = () =>
        {
            var instance = ServiceProvider.DefaultInstance;
        };

        gettingDefaultInstance
            .Should()
            .Throw<NullReferenceException>();
    }

    private void GlobalStateResetter()
    {
        var allStaticNonpublicFields = typeof(ServiceProvider).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
        var defaultInstanceFieldInfo = allStaticNonpublicFields.FirstOrDefault(field => field.Name.Equals("s_defaultInstance"));
        if (defaultInstanceFieldInfo is null)
        {
            return;
        }
        defaultInstanceFieldInfo.SetValue(null, null);
    }
}