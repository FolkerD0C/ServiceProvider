namespace FolkerD0C.DependencyInjection;

internal static class Extensions
{
    internal static TContract GuardedCast<TContract>(this object? value)
    {
        if (value is TContract contractValue)
        {
            return contractValue;
        }
        throw new InvalidOperationException($"Object [{value}] can not be cast to {typeof(TContract)}.");
    }
}