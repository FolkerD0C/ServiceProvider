namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Defines an interface for objects that can be reset to their initial state.
/// </summary>
public interface IResettable
{
    /// <summary>
    /// Resets the object to its original state
    /// </summary>
    void Reset();
}
