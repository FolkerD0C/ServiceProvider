namespace FolkerD0C.DependencyInjection;

public interface IServiceProvider
{
    public TContract Resolve<TContract>();
}