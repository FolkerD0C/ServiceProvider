using FluentAssertions;
using FolkerD0C.DependencyInjection.Collections;
using FolkerD0C.DependencyInjection.Exceptions;

namespace FolkerD0C.DependencyInjection.Tests;

public class ServiceProviderBuilderCollectionShould
{

    [Fact]
    public void ThrowWhenKeyIsInvalid()
    {
        string? invalidKey = null;

        var invalidKeyAssertion = ServiceProviderBuilderCollection.DefaultBuilderCollection
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
        ServiceProviderBuilderCollection.DefaultBuilder.Should().Be(ServiceProviderBuilder.DefaultInstance);
    }
}
