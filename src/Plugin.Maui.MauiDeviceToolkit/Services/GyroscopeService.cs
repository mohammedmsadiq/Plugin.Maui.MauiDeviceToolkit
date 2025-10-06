using MauiGyroscope = Microsoft.Maui.Devices.Sensors.Gyroscope;
using MauiSensorSpeed = Microsoft.Maui.Devices.Sensors.SensorSpeed;
using MauiGyroscopeChangedEventArgs = Microsoft.Maui.Devices.Sensors.GyroscopeChangedEventArgs;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IGyroscopeService using MAUI Essentials
/// </summary>
public class GyroscopeService : Interfaces.IGyroscopeService
{
    /// <inheritdoc/>
    public bool IsMonitoring => MauiGyroscope.Default.IsMonitoring;

    /// <inheritdoc/>
    public event EventHandler<Interfaces.GyroscopeChangedEventArgs>? ReadingChanged;

    /// <inheritdoc/>
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI)
    {
        if (!IsMonitoring)
        {
            MauiGyroscope.Default.ReadingChanged += OnReadingChanged;
            MauiGyroscope.Default.Start(MapSensorSpeed(speed));
        }
    }

    /// <inheritdoc/>
    public void Stop()
    {
        if (IsMonitoring)
        {
            MauiGyroscope.Default.Stop();
            MauiGyroscope.Default.ReadingChanged -= OnReadingChanged;
        }
    }

    private void OnReadingChanged(object? sender, MauiGyroscopeChangedEventArgs e)
    {
        var angularVelocity = new Interfaces.Vector3(e.Reading.AngularVelocity.X, e.Reading.AngularVelocity.Y, e.Reading.AngularVelocity.Z);
        ReadingChanged?.Invoke(this, new Interfaces.GyroscopeChangedEventArgs(angularVelocity));
    }

    private static MauiSensorSpeed MapSensorSpeed(Interfaces.SensorSpeed speed)
    {
        return speed switch
        {
            Interfaces.SensorSpeed.Fastest => MauiSensorSpeed.Fastest,
            Interfaces.SensorSpeed.Game => MauiSensorSpeed.Game,
            Interfaces.SensorSpeed.UI => MauiSensorSpeed.UI,
            _ => MauiSensorSpeed.Default
        };
    }
}
