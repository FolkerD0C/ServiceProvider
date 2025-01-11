namespace FolkerD0C.DependencyInjection.Tests.Shared;

public static class ServiceResponses
{
    public static readonly Guid GuidResponse = Guid.NewGuid();

    public static readonly string StringResponse = Guid.NewGuid().ToString();

    public static readonly Guid OtherGuidResponse = Guid.NewGuid();

    public static readonly string OtherStringResponse = Guid.NewGuid().ToString();
}
