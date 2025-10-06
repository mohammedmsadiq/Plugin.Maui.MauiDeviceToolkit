using MauiMagnetometer = Microsoft.Maui.Devices.Sensors.Magnetometer;
using MauiSensorSpeed = Microsoft.Maui.Devices.Sensors.SensorSpeed;
using MauiMagnetometerChangedEventArgs = Microsoft.Maui.Devices.Sensors.MagnetometerChangedEventArgs;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IMagnetometerService using MAUI Essentials
/// </summary>
public class MagnetometerService : Interfaces.IMagnetometerService
{
    /// <inheritdoc/>
    public bool IsMonitoring => MauiMagnetometer.Default.IsMonitoring;

    /// <inheritdoc/>
    public event EventHandler<Interfaces.MagnetometerChangedEventArgs>? ReadingChanged;

    /// <inheritdoc/>
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI)
    {
        if (!IsMonitoring)
        {
            MauiMagnetometer.Default.ReadingChanged += OnReadingChanged;
            MauiMagnetometer.Default.Start(MapSensorSpeed(speed));
        }
    }

    /// <inheritdoc/>
    public void Stop()
    {
        if (IsMonitoring)
        {
            MauiMagnetometer.Default.Stop();
            MauiMagnetometer.Default.ReadingChanged -= OnReadingChanged;
        }
    }

    private void OnReadingChanged(object? sender, MauiMagnetometerChangedEventArgs e)
    {
        var magneticField = new Interfaces.Vector3(e.Reading.MagneticField.X, e.Reading.MagneticField.Y, e.Reading.MagneticField.Z);
        ReadingChanged?.Invoke(this, new Interfaces.MagnetometerChangedEventArgs(magneticField));
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
