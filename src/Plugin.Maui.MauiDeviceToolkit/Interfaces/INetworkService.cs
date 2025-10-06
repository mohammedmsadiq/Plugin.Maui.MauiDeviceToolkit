namespace Plugin.Maui.MauiDeviceToolkit.Interfaces;

/// <summary>
/// Service for monitoring network connectivity
/// </summary>
public interface INetworkService
{
    /// <summary>
    /// Gets the current network access state
    /// </summary>
    NetworkAccess NetworkAccess { get; }

    /// <summary>
    /// Gets the current connection profiles
    /// </summary>
    IEnumerable<ConnectionProfile> ConnectionProfiles { get; }

    /// <summary>
    /// Checks if the device has internet connectivity
    /// </summary>
    /// <returns>True if connected to internet</returns>
    bool IsConnected { get; }

    /// <summary>
    /// Event raised when connectivity changes
    /// </summary>
    event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;
}

/// <summary>
/// Network access state enumeration
/// </summary>
public enum NetworkAccess
{
    /// <summary>
    /// Network access status is unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// No network access
    /// </summary>
    None,

    /// <summary>
    /// Local network access only
    /// </summary>
    Local,

    /// <summary>
    /// Limited internet access
    /// </summary>
    ConstrainedInternet,

    /// <summary>
    /// Full internet access
    /// </summary>
    Internet
}

/// <summary>
/// Connection profile enumeration
/// </summary>
public enum ConnectionProfile
{
    /// <summary>
    /// Bluetooth connection
    /// </summary>
    Bluetooth,

    /// <summary>
    /// Cellular connection
    /// </summary>
    Cellular,

    /// <summary>
    /// Ethernet connection
    /// </summary>
    Ethernet,

    /// <summary>
    /// WiFi connection
    /// </summary>
    WiFi,

    /// <summary>
    /// Unknown connection type
    /// </summary>
    Unknown
}

/// <summary>
/// Event arguments for connectivity changes
/// </summary>
public class ConnectivityChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the new network access state
    /// </summary>
    public NetworkAccess NetworkAccess { get; }

    /// <summary>
    /// Gets the new connection profiles
    /// </summary>
    public IEnumerable<ConnectionProfile> ConnectionProfiles { get; }

    /// <summary>
    /// Initializes a new instance of the ConnectivityChangedEventArgs class
    /// </summary>
    public ConnectivityChangedEventArgs(NetworkAccess networkAccess, IEnumerable<ConnectionProfile> profiles)
    {
        NetworkAccess = networkAccess;
        ConnectionProfiles = profiles;
    }
}
