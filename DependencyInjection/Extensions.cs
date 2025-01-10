namespace FolkerD0C.DependencyInjection;

internal static class Extensions
{
    internal static TContract GuardedCast<TContract>(this object? value)
    {
        if (value is TContract contractValue)
        {
            return contractValue;
        }
        throw new ServiceTypeNotCastableException(typeof(TContract), value?.GetType() ?? typeof(object));
    }
}