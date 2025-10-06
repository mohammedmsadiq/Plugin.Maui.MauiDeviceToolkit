namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of ISensorService
/// </summary>
public class SensorService : Interfaces.ISensorService
{
    private readonly Lazy<Interfaces.IBatteryInfo> _battery;
    private readonly Lazy<Interfaces.IGeolocationService> _geolocation;
    private readonly Lazy<Interfaces.IOrientationService> _orientation;
    private readonly Lazy<Interfaces.IAccelerometerService> _accelerometer;
    private readonly Lazy<Interfaces.IGyroscopeService> _gyroscope;
    private readonly Lazy<Interfaces.IMagnetometerService> _magnetometer;

    /// <summary>
    /// Initializes a new instance of the SensorService class
    /// </summary>
    public SensorService()
    {
        _battery = new Lazy<Interfaces.IBatteryInfo>(() => new BatteryInfoService());
        _geolocation = new Lazy<Interfaces.IGeolocationService>(() => new GeolocationService());
        _orientation = new Lazy<Interfaces.IOrientationService>(() => new OrientationSensorService());
        _accelerometer = new Lazy<Interfaces.IAccelerometerService>(() => new AccelerometerService());
        _gyroscope = new Lazy<Interfaces.IGyroscopeService>(() => new GyroscopeService());
        _magnetometer = new Lazy<Interfaces.IMagnetometerService>(() => new MagnetometerService());
    }

    /// <inheritdoc/>
    public Interfaces.IBatteryInfo Battery => _battery.Value;

    /// <inheritdoc/>
    public Interfaces.IGeolocationService Geolocation => _geolocation.Value;

    /// <inheritdoc/>
    public Interfaces.IOrientationService Orientation => _orientation.Value;

    /// <inheritdoc/>
    public Interfaces.IAccelerometerService Accelerometer => _accelerometer.Value;

    /// <inheritdoc/>
    public Interfaces.IGyroscopeService Gyroscope => _gyroscope.Value;

    /// <inheritdoc/>
    public Interfaces.IMagnetometerService Magnetometer => _magnetometer.Value;
}
