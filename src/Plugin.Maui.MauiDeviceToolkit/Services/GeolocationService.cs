using MauiGeolocation = Microsoft.Maui.Devices.Sensors.Geolocation;
using MauiLocation = Microsoft.Maui.Devices.Sensors.Location;
using MauiGeolocationRequest = Microsoft.Maui.Devices.Sensors.GeolocationRequest;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IGeolocationService using MAUI Essentials
/// </summary>
public class GeolocationService : Interfaces.IGeolocationService
{
    /// <inheritdoc/>
    public async Task<Interfaces.Location?> GetLastKnownLocationAsync()
    {
        try
        {
            var location = await MauiGeolocation.Default.GetLastKnownLocationAsync();
            return MapLocation(location);
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<Interfaces.Location?> GetLocationAsync(Interfaces.GeolocationRequest? request = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var mauiRequest = request != null ? MapRequest(request) : new MauiGeolocationRequest();
            var location = await MauiGeolocation.Default.GetLocationAsync(mauiRequest, cancellationToken);
            return MapLocation(location);
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> IsLocationEnabledAsync()
    {
        try
        {
            var location = await MauiGeolocation.Default.GetLastKnownLocationAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static MauiGeolocationRequest MapRequest(Interfaces.GeolocationRequest request)
    {
        var mauiRequest = new MauiGeolocationRequest
        {
            Timeout = request.Timeout
        };

        mauiRequest.RequestFullAccuracy = request.DesiredAccuracy switch
        {
            Interfaces.GeolocationAccuracy.Best or Interfaces.GeolocationAccuracy.High => true,
            _ => false
        };

        return mauiRequest;
    }

    private static Interfaces.Location? MapLocation(MauiLocation? location)
    {
        if (location == null)
            return null;

        return new Interfaces.Location
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Altitude = location.Altitude,
            Accuracy = location.Accuracy,
            Timestamp = location.Timestamp,
            Speed = location.Speed,
            Course = location.Course
        };
    }
}
