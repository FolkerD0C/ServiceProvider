namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Defines a contract for objects that have an expiration scope,
/// specifying their ability to be renewed and their current validity state.
/// </summary>
public interface IExpirationScope
{
    /// <summary>
    /// Gets a value indicating whether the object can be renewed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the object can be renewed; otherwise, <c>false</c>.
    /// </value>
    bool IsRenewable { get; }

    /// <summary>
    /// Gets a value indicating whether the object is currently valid.
    /// </summary>
    /// <value>
    /// <c>true</c> if the object is valid at the current time; otherwise, <c>false</c>.
    /// </value>
    bool IsValidCurrently { get; }

    /// <summary>
    /// Renews the object, resetting or extending its validity period.
    /// </summary>
    void Renew();
}

