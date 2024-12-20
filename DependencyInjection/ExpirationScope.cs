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
        private readonly bool _isValid;

        internal FixedExpirationScope(bool isValid)
        {
            _isValid = isValid;
        }

        public bool IsRenewable
            => _isValid;

        public bool IsValidCurrently
            => _isValid;

        public void Renew()
        {
        }
    }
}
