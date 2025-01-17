namespace FolkerD0C.DependencyInjection.Tests;

public abstract class TestBase
{
#if CAN_RESET_GLOBAL_STATE
    private static readonly object s_globalStateLock = Guid.NewGuid();

    protected void ResetGlobalState()
    {
        lock (s_globalStateLock)
        {
            ServiceProvider.ResetGlobalState();
            ServiceProviderBuilder.ResetGlobalState();
            Collections.ServiceProviderCollection.ResetGlobalState();
            Collections.ServiceProviderBuilderCollection.ResetGlobalState();
        }
    }
#endif
}