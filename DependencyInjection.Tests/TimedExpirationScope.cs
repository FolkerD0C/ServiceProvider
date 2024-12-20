namespace FolkerD0C.DependencyInjection.Tests;

public class TimedExpirationScope : IExpirationScope
{
    private readonly TimeSpan _validTimespan;
    DateTime _expirationStart;

    public TimedExpirationScope(int validMilliSeconds) : this(TimeSpan.FromMilliseconds(validMilliSeconds))
    {
    }

    public TimedExpirationScope(TimeSpan timeSpan)
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
