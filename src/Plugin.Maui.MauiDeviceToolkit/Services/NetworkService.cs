using MauiConnectivity = Microsoft.Maui.Networking.Connectivity;
using MauiNetworkAccess = Microsoft.Maui.Networking.NetworkAccess;
using MauiConnectionProfile = Microsoft.Maui.Networking.ConnectionProfile;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of INetworkService using MAUI Essentials
/// </summary>
public class NetworkService : Interfaces.INetworkService
{
    /// <inheritdoc/>
    public Interfaces.NetworkAccess NetworkAccess => MapNetworkAccess(MauiConnectivity.Current.NetworkAccess);

    /// <inheritdoc/>
    public IEnumerable<Interfaces.ConnectionProfile> ConnectionProfiles =>
        MauiConnectivity.Current.ConnectionProfiles.Select(MapConnectionProfile);

    /// <inheritdoc/>
    public bool IsConnected => MauiConnectivity.Current.NetworkAccess == MauiNetworkAccess.Internet;

    /// <inheritdoc/>
    public event EventHandler<Interfaces.ConnectivityChangedEventArgs>? ConnectivityChanged;

    /// <summary>
    /// Initializes a new instance of the NetworkService class
    /// </summary>
    public NetworkService()
    {
        MauiConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
    }

    private void OnConnectivityChanged(object? sender, Microsoft.Maui.Networking.ConnectivityChangedEventArgs e)
    {
        var args = new Interfaces.ConnectivityChangedEventArgs(
            MapNetworkAccess(e.NetworkAccess),
            e.ConnectionProfiles.Select(MapConnectionProfile));

        ConnectivityChanged?.Invoke(this, args);
    }

    private static Interfaces.NetworkAccess MapNetworkAccess(MauiNetworkAccess networkAccess)
    {
        return networkAccess switch
        {
            MauiNetworkAccess.Unknown => Interfaces.NetworkAccess.Unknown,
            MauiNetworkAccess.None => Interfaces.NetworkAccess.None,
            MauiNetworkAccess.Local => Interfaces.NetworkAccess.Local,
            MauiNetworkAccess.ConstrainedInternet => Interfaces.NetworkAccess.ConstrainedInternet,
            MauiNetworkAccess.Internet => Interfaces.NetworkAccess.Internet,
            _ => Interfaces.NetworkAccess.Unknown
        };
    }

    private static Interfaces.ConnectionProfile MapConnectionProfile(MauiConnectionProfile profile)
    {
        return profile switch
        {
            MauiConnectionProfile.Bluetooth => Interfaces.ConnectionProfile.Bluetooth,
            MauiConnectionProfile.Cellular => Interfaces.ConnectionProfile.Cellular,
            MauiConnectionProfile.Ethernet => Interfaces.ConnectionProfile.Ethernet,
            MauiConnectionProfile.WiFi => Interfaces.ConnectionProfile.WiFi,
            _ => Interfaces.ConnectionProfile.Unknown
        };
    }
}
