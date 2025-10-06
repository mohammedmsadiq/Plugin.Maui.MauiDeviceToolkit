using MauiOrientationSensor = Microsoft.Maui.Devices.Sensors.OrientationSensor;
using MauiSensorSpeed = Microsoft.Maui.Devices.Sensors.SensorSpeed;
using MauiOrientationSensorChangedEventArgs = Microsoft.Maui.Devices.Sensors.OrientationSensorChangedEventArgs;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IOrientationService using MAUI Essentials
/// </summary>
public class OrientationSensorService : Interfaces.IOrientationService
{
    /// <inheritdoc/>
    public bool IsMonitoring => MauiOrientationSensor.Default.IsMonitoring;

    /// <inheritdoc/>
    public event EventHandler<Interfaces.OrientationChangedEventArgs>? OrientationChanged;

    /// <inheritdoc/>
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI)
    {
        if (!IsMonitoring)
        {
            MauiOrientationSensor.Default.ReadingChanged += OnReadingChanged;
            MauiOrientationSensor.Default.Start(MapSensorSpeed(speed));
        }
    }

    /// <inheritdoc/>
    public void Stop()
    {
        if (IsMonitoring)
        {
            MauiOrientationSensor.Default.Stop();
            MauiOrientationSensor.Default.ReadingChanged -= OnReadingChanged;
        }
    }

    private void OnReadingChanged(object? sender, MauiOrientationSensorChangedEventArgs e)
    {
        var orientation = new Interfaces.Quaternion
        {
            X = e.Reading.Orientation.X,
            Y = e.Reading.Orientation.Y,
            Z = e.Reading.Orientation.Z,
            W = e.Reading.Orientation.W
        };

        OrientationChanged?.Invoke(this, new Interfaces.OrientationChangedEventArgs(orientation));
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
