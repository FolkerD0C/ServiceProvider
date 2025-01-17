using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Tests.Shared.Services;

namespace FolkerD0C.DependencyInjection.Tests;

[Collection("All tests")]
public class ServiceProviderCollectionShould : TestBase
{
    [Fact]
    public void AllowMultipleOfTheSameContractTypeInDifferentProviders()
    {
        var fixture = new Fixture();
        var key1 = fixture.Create<object>();
        var key2 = fixture.Create<string>();

        var expectedServiceResponse1 = Guid.NewGuid();
        var expectedServiceResponse2 = Guid.NewGuid();

        var builderCollection = new ServiceProviderBuilderCollection();
        builderCollection.GetBuilder(key1)
            .AddSingleton(() => new GetterService<Guid>(expectedServiceResponse1));
        builderCollection.GetBuilder(key2)
            .AddSingleton(() => new GetterService<Guid>(expectedServiceResponse2));
        var sut = builderCollection.BuildAll();

        sut.GetServiceProvider(key1)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(expectedServiceResponse1);

        sut.GetServiceProvider(key2)
            .Resolve<GetterService<Guid>>()
            .GetValue()
            .Should()
            .Be(expectedServiceResponse2);
    }

    [Fact]
    public void ThrowWhenProviderIsAddedWithExistingKey()
    {
        var key =Guid.NewGuid();
        var builderCollection = new ServiceProviderBuilderCollection();
        builderCollection.GetBuilder(key);
        var sut = builderCollection.BuildAll();
        var provider = new ServiceProviderBuilder().Build();

        var alreadyExistsAssertion = sut.Invoking(subject => subject.AddServiceProvider(key, provider))
            .Should()
            .Throw<ServiceProviderAlreadyExistsException>();
        
        alreadyExistsAssertion.Subject.First().ServiceProviderKey.Should().Be(key);
    }

    [Fact]
    public void ThrowWhenKeyIsNotRegistered()
    {
        var nonExistentKey = Guid.NewGuid();
        var sut = new ServiceProviderBuilderCollection().BuildAll();

        var notFoundAssertion = sut
            .Invoking(subject => subject.GetServiceProvider(nonExistentKey))
            .Should()
            .Throw<ServiceProviderNotFoundException>();
        
        notFoundAssertion.Subject.First().ServiceProviderKey.Should().Be(nonExistentKey);
    }

    [Fact]
    public void ThrowIfKeyIsInvalid()
    {
        string? invalidKey = null;
        var sut = new ServiceProviderBuilderCollection().BuildAll();
        var provider = new ServiceProviderBuilder().Build();

        var invalidKeyAddingAssertion = sut
#pragma warning disable CS8604
            .Invoking(subject => subject.AddServiceProvider(invalidKey, provider))
#pragma warning restore CS8604
            .Should()
            .Throw<InvalidServiceProviderKeyException>();
        invalidKeyAddingAssertion.Subject.First().InvalidKey.Should().Be(invalidKey);

        var invalidKeyGettingAssertion = sut
#pragma warning disable CS8604
            .Invoking(subject => subject.GetServiceProvider(invalidKey))
#pragma warning restore CS8604
            .Should()
            .Throw<InvalidServiceProviderKeyException>();
        invalidKeyGettingAssertion.Subject.First().InvalidKey.Should().Be(invalidKey);
    }

    [Fact]
    public void ThrowIfProviderIsInvalidUponAdding()
    {
        var fixture = new Fixture();
        var dummyKey = fixture.Create<object>();
        var sut = new ServiceProviderBuilderCollection().BuildAll();
        

        var invalidKeyAddingAssertion = sut
#pragma warning disable CS8625
            .Invoking(subject => subject.AddServiceProvider(dummyKey, null))
#pragma warning restore CS8625
            .Should()
            .Throw<InvalidServiceProviderException>();
        invalidKeyAddingAssertion.Subject.First().ServiceProvider.Should().BeNull();
    }

    [Theory]
    [InlineData("dummy", true)]
    [InlineData(null, false)]
    public void ReturnExpectedAndNotThrowWhenTryAddIsCalled(object? key, bool expected)
    {
        var sut = new ServiceProviderBuilderCollection().BuildAll();

        #pragma warning disable CS8604
        bool providerAdded = sut.TryAddServiceProvider(key, new ServiceProviderBuilder().Build());
        #pragma warning restore CS8604

        providerAdded.Should().Be(expected);
    }

#if CAN_RESET_GLOBAL_STATE
    [Fact]
    public void ReturnSameDefaultAsProviderClass()
    {
        ResetGlobalState();
        ServiceProviderBuilder.BuildDefault();
        ServiceProvider.DefaultProvider.Should().Be(ServiceProvider.DefaultProvider);
    }

    [Fact]
    public void ReturnSameDefaultEveryTime()
    {
        ResetGlobalState();
        var firstCall = ServiceProviderCollection.DefaultProviderCollection;
        var secondCall = ServiceProviderCollection.DefaultProviderCollection;

        secondCall.Should().Be(firstCall);
    }

    [Fact]
    public void HaveTheSameProvidersAsBuilderCollectionWhenDefault()
    {
        ResetGlobalState();
        Random rng = new();
        int providerCount = rng.Next(10, 20);
        var keys = Enumerable.Range(0, providerCount).Select(_ => Guid.NewGuid()).ToList();

        foreach (var key in keys)
        {
            ServiceProviderBuilderCollection.DefaultBuilderCollection
                .GetBuilder(key)
                .AddSingleton(() => new GetterService<Guid>(key));
        }
        ServiceProviderBuilderCollection.DefaultBuilderCollection.BuildAll();
        
        foreach (var key in keys)
        {
            ServiceProviderCollection.DefaultProviderCollection
                .GetServiceProvider(key)
                .Resolve<GetterService<Guid>>()
                .GetValue()
                .Should()
                .Be(key);
        }
    }
#endif
}
