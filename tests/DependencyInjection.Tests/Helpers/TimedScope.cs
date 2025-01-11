namespace FolkerD0C.DependencyInjection.Tests.Helpers;

public class TimedScope : IServiceScope
{
    private readonly TimeSpan _validTimespan;
    DateTime _expirationStart;

    public TimedScope(int validMilliSeconds) : this(TimeSpan.FromMilliseconds(validMilliSeconds))
    {
    }

    public TimedScope(TimeSpan timeSpan)
    {
        _validTimespan = timeSpan;
        _expirationStart = DateTime.Now;
    }

    public bool IsRenewable => true;

    public bool IsValidCurrently => (DateTime.Now - _expirationStart) < _validTimespan;

    public void Renew()
    {
        _expirationStart = DateTime.Now;
    }
}
