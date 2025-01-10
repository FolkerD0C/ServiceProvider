namespace FolkerD0C.DependencyInjection;

internal static class ServiceScope
{
    internal static IServiceScope GetSingleton()
    {
        return new FixedServiceScope(true);
    }

    internal static IServiceScope GetTransient()
    {
        return new FixedServiceScope(false);
    }

    private class FixedServiceScope : IServiceScope
    {
        private readonly bool _isValid;

        internal FixedServiceScope(bool isValid)
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
