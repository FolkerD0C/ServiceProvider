namespace FolkerD0C.DependencyInjection;

/// <summary>
/// Defines a contract for objects that have an expiration scope.
/// If a scope is expired then the object is renewed.
/// If a scope is renewable then it gets renewed.
/// </summary>
public interface IExpirationScope
{
    /// <summary>
    /// Gets a value indicating whether the scope can be renewed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the scope can be renewed; otherwise, <c>false</c>.
    /// </value>
    bool IsRenewable { get; }

    /// <summary>
    /// Gets a value indicating whether the scope is currently valid.
    /// </summary>
    /// <value>
    /// <c>true</c> if the scope is valid at the current time; otherwise, <c>false</c>.
    /// </value>
    bool IsValidCurrently { get; }

    /// <summary>
    /// Renews the scope, resetting or extending its validity period.
    /// </summary>
    void Renew();
}

