using AutoFixture;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Exceptions;
using FolkerD0C.DependencyInjection.Tests.Services;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderCollectionShould
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

        var notFoundAssertion = ServiceProviderCollection.DefaultProviderCollection
            .Invoking(subject => subject.GetServiceProvider(nonExistentKey))
            .Should()
            .Throw<ServiceProviderNotFoundException>();
        
        notFoundAssertion.Subject.First().ServiceProviderKey.Should().Be(nonExistentKey);
    }

    [Fact]
    public void ReturnSameDefaultAsProviderClass()
    {
        ServiceProvider.BuildDefaultProvider();
        ServiceProviderCollection.DefaultProvider.Should().Be(ServiceProvider.DefaultInstance);
    }
}
