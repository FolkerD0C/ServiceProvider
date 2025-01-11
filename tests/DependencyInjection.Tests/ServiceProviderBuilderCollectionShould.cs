using System.Reflection;
using FluentAssertions;
using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Exceptions;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderBuilderCollectionShould : TestBase
{

    [Fact]
    public void ThrowWhenKeyIsInvalid()
    {
        string? invalidKey = null;
        var sut = new ServiceProviderBuilderCollection();

        var invalidKeyAssertion = sut
#pragma warning disable CS8604
            .Invoking(subject => subject.GetBuilder(invalidKey))
#pragma warning restore CS8604
            .Should()
            .Throw<InvalidServiceProviderKeyException>();
        
        invalidKeyAssertion.Subject.First().InvalidKey.Should().Be(invalidKey);
    }

    [Fact]
    public void ThrowIfProviderCollectionHasBeenBuilt()
    {
        var unusedKey = Guid.NewGuid();
        var sut = new ServiceProviderBuilderCollection();
        sut.BuildAll();

        sut.Invoking(subject => subject.BuildAll())
            .Should()
            .Throw<ServiceProvidersHaveBeenBuiltException>();
        sut.Invoking(subject => subject.GetBuilder(unusedKey))
            .Should()
            .Throw<ServiceProvidersHaveBeenBuiltException>();
    }

    [Fact]
    public void ReturnExistingBuilder()
    {
        var sut = new ServiceProviderBuilderCollection();
        var key = Guid.NewGuid();
        var expectedBuilder = sut.GetBuilder(key);

        sut.GetBuilder(key).Should().Be(expectedBuilder);
    }

    [Fact]
    public void ReturnSameDefaultAsBuilderClass()
    {
        ServiceProviderBuilderCollection.DefaultBuilder.Should().Be(ServiceProviderBuilder.DefaultBuilder);
    }

    [Fact]
    public void BeEmptyWhenCreated()
    {
        var expectedServiceProviderCount = 0;
        var sut = new ServiceProviderBuilderCollection();

        sut.BuildAll().GetServiceProviders().Count().Should().Be(expectedServiceProviderCount);
    }

    [Fact]
    public void ReturnSameDefaultEveryTime()
    {
        ResetGlobalState();
        var firstCall = ServiceProviderBuilderCollection.DefaultBuilderCollection;
        var secondCall = ServiceProviderBuilderCollection.DefaultBuilderCollection;

        secondCall.Should().Be(firstCall);
    }

    [Fact]
    public void CreateProviderCollectionWithAllAddedProviders()
    {
        Random rng = new();
        int expectedProviderCount = rng.Next(10, 20);
        IEnumerable<Guid> allKeys = Enumerable
            .Range(0, expectedProviderCount)
            .Select(_ => Guid.NewGuid());
        var sut = new ServiceProviderBuilderCollection();

        foreach (var key in allKeys)
        {
            sut.GetBuilder(key);
        }

        sut.BuildAll().GetServiceProviders().Count().Should().Be(expectedProviderCount);
    }

    [Fact]
    public void BeEmptyWhenDefault()
    {
        ResetGlobalState();
        int expectedProviderCollectionCount = 0;

        ServiceProviderBuilderCollection.DefaultBuilderCollection
            .BuildAll()
            .GetServiceProviders()
            .Count()
            .Should()
            .Be(expectedProviderCollectionCount);
    }

    [Fact]
    public void BeEmptyWhenResetAndNotThrowWhenBuiltAgain()
    {
        int excpectedServiceCount = 0;
        var sut = new ServiceProviderBuilderCollection();
        sut.GetBuilder(Guid.NewGuid());

        sut.Reset();
        var providerCollection = sut.BuildAll();

        providerCollection.GetServiceProviders()
            .Count()
            .Should()
            .Be(excpectedServiceCount);

        sut.Reset();
        sut.Invoking(subject => subject.BuildAll())
            .Should()
            .NotThrow<ServiceProvidersHaveBeenBuiltException>();
    }
}
