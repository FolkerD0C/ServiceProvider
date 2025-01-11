namespace FolkerD0C.DependencyInjection.Tests.Shared.Services;

public class TestService : IGuidGetterService, IStringGetterService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetGuid()
        => _id;

    public string GetString()
    => _id.ToString();
}

public interface IGuidGetterService
{
    Guid GetGuid();
}

public interface IStringGetterService
{
    string GetString();
}
