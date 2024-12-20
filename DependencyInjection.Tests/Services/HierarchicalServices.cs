namespace FolkerD0C.DependencyInjection.Tests.Services;

public interface IMiddleManService
{
    IGuidGetterService GetGuidGetterService();
}

public interface IOtherMiddleManService
{
    IStringGetterService GetStringGetterService();
}

public interface ITopLevelService
{
    IMiddleManService GetMiddleManService();

    IOtherMiddleManService GetOtherMiddleManService();
}

public class MiddleManService : IMiddleManService
{
    private readonly IGuidGetterService _guidGetterService;

    public MiddleManService(IGuidGetterService guidGetterService)
    {
        _guidGetterService = guidGetterService;
    }

    public IGuidGetterService GetGuidGetterService()
        => _guidGetterService;
}

public class OtherMiddleManService : IOtherMiddleManService
{
    private readonly IStringGetterService _stringGetterService;

    public OtherMiddleManService(IStringGetterService stringGetterService)
    {
        _stringGetterService = stringGetterService;
    }

    public IStringGetterService GetStringGetterService()
        => _stringGetterService;
}

public class TopLevelService : ITopLevelService
{
    private readonly IMiddleManService _middleManService;
    private readonly IOtherMiddleManService _otherMiddleManService;

    public TopLevelService(IMiddleManService middleManService, IOtherMiddleManService otherMiddleManService)
    {
        _middleManService = middleManService;
        _otherMiddleManService = otherMiddleManService;
    }

    public IMiddleManService GetMiddleManService()
        => _middleManService;

    public IOtherMiddleManService GetOtherMiddleManService()
        => _otherMiddleManService;
}
