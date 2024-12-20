namespace FolkerD0C.DependencyInjection;

public interface IExpirationScope
{
    bool IsValidCurrently();

    void Renew();
}
