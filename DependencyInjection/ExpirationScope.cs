namespace FolkerD0C.DependencyInjection;

internal static class ExpirationScope
{
    internal static IExpirationScope GetSingleton()
    {
        return new FixedExpirationScope(true);
    }

    internal static IExpirationScope GetTransient()
    {
        return new FixedExpirationScope(false);
    }

    private class FixedExpirationScope : IExpirationScope
    {
        private readonly bool _isExpired;

        internal FixedExpirationScope(bool isExpired)
        {
            _isExpired = isExpired;
        }

        public bool IsValidCurrently()
        {
            return _isExpired;
        }

        public void Renew()
        {
        }
    }
}
