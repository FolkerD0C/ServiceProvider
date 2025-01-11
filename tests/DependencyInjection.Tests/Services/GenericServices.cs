namespace FolkerD0C.DependencyInjection.Tests.Services;

public class GenericService<T, U> : IOtherGenericService<T, U>
{
    private readonly GetterService<T> _tGetterService;
    private readonly GetterService<U> _uGetterService;

    public GenericService(GetterService<T> tGetterService, GetterService<U> uGetterService)
    {
        _tGetterService = tGetterService;
        _uGetterService = uGetterService;
    }

    public T GetT()
        => _tGetterService.GetValue();

    public U GetU()
        => _uGetterService.GetValue();
}

public class SpecificGenericService : IGenericService<Guid>
{
    private readonly GetterService<Guid> _guidGetterService;

    public SpecificGenericService(GetterService<Guid> guidGetterService)
    {
        _guidGetterService = guidGetterService;
    }

    public Guid GetT()
        => _guidGetterService.GetValue();
}

public interface IGenericService<T>
{
    T GetT();
}

public interface IOtherGenericService<T, U> : IGenericService<T>
{
    U GetU();
}

public class GetterService<T>
{
    private readonly T _value;

    public GetterService(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }
}
