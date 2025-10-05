using MauiAccelerometer = Microsoft.Maui.Devices.Sensors.Accelerometer;
using MauiSensorSpeed = Microsoft.Maui.Devices.Sensors.SensorSpeed;
using MauiAccelerometerChangedEventArgs = Microsoft.Maui.Devices.Sensors.AccelerometerChangedEventArgs;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IAccelerometerService using MAUI Essentials
/// </summary>
public class AccelerometerService : Interfaces.IAccelerometerService
{
    /// <inheritdoc/>
    public bool IsMonitoring => MauiAccelerometer.Default.IsMonitoring;

    /// <inheritdoc/>
    public event EventHandler<Interfaces.AccelerometerChangedEventArgs>? ReadingChanged;

    /// <inheritdoc/>
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI)
    {
        if (!IsMonitoring)
        {
            MauiAccelerometer.Default.ReadingChanged += OnReadingChanged;
            MauiAccelerometer.Default.Start(MapSensorSpeed(speed));
        }
    }

    /// <inheritdoc/>
    public void Stop()
    {
        if (IsMonitoring)
        {
            MauiAccelerometer.Default.Stop();
            MauiAccelerometer.Default.ReadingChanged -= OnReadingChanged;
        }
    }

    private void OnReadingChanged(object? sender, MauiAccelerometerChangedEventArgs e)
    {
        var acceleration = new Interfaces.Vector3(e.Reading.Acceleration.X, e.Reading.Acceleration.Y, e.Reading.Acceleration.Z);
        ReadingChanged?.Invoke(this, new Interfaces.AccelerometerChangedEventArgs(acceleration));
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
