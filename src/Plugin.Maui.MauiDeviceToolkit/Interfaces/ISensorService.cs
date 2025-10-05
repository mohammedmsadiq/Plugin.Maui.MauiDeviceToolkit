namespace Plugin.Maui.MauiDeviceToolkit.Interfaces;

/// <summary>
/// Service for accessing device sensors (GPS, Battery, Orientation, etc.)
/// </summary>
public interface ISensorService
{
    /// <summary>
    /// Gets the current battery information
    /// </summary>
    IBatteryInfo Battery { get; }

    /// <summary>
    /// Gets the current geolocation service
    /// </summary>
    IGeolocationService Geolocation { get; }

    /// <summary>
    /// Gets the device orientation service
    /// </summary>
    IOrientationService Orientation { get; }

    /// <summary>
    /// Gets the accelerometer service
    /// </summary>
    IAccelerometerService Accelerometer { get; }

    /// <summary>
    /// Gets the gyroscope service
    /// </summary>
    IGyroscopeService Gyroscope { get; }

    /// <summary>
    /// Gets the magnetometer (compass) service
    /// </summary>
    IMagnetometerService Magnetometer { get; }
}

/// <summary>
/// Battery information interface
/// </summary>
public interface IBatteryInfo
{
    /// <summary>
    /// Gets the current battery charge level (0.0 to 1.0)
    /// </summary>
    double ChargeLevel { get; }

    /// <summary>
    /// Gets the current battery state
    /// </summary>
    BatteryState State { get; }

    /// <summary>
    /// Gets the battery power source
    /// </summary>
    BatteryPowerSource PowerSource { get; }

    /// <summary>
    /// Event raised when battery information changes
    /// </summary>
    event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged;
}

/// <summary>
/// Battery state enumeration
/// </summary>
public enum BatteryState
{
    /// <summary>
    /// Battery state is unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// Battery is charging
    /// </summary>
    Charging,

    /// <summary>
    /// Battery is discharging
    /// </summary>
    Discharging,

    /// <summary>
    /// Battery is full
    /// </summary>
    Full,

    /// <summary>
    /// Battery is not charging
    /// </summary>
    NotCharging,

    /// <summary>
    /// No battery present
    /// </summary>
    NotPresent
}

/// <summary>
/// Battery power source enumeration
/// </summary>
public enum BatteryPowerSource
{
    /// <summary>
    /// Power source is unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// Running on battery
    /// </summary>
    Battery,

    /// <summary>
    /// Connected to AC power
    /// </summary>
    AC,

    /// <summary>
    /// Connected to USB
    /// </summary>
    Usb,

    /// <summary>
    /// Wireless charging
    /// </summary>
    Wireless
}

/// <summary>
/// Event arguments for battery information changes
/// </summary>
public class BatteryInfoChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the new battery charge level
    /// </summary>
    public double ChargeLevel { get; }

    /// <summary>
    /// Gets the new battery state
    /// </summary>
    public BatteryState State { get; }

    /// <summary>
    /// Gets the new power source
    /// </summary>
    public BatteryPowerSource PowerSource { get; }

    /// <summary>
    /// Initializes a new instance of the BatteryInfoChangedEventArgs class
    /// </summary>
    public BatteryInfoChangedEventArgs(double chargeLevel, BatteryState state, BatteryPowerSource powerSource)
    {
        ChargeLevel = chargeLevel;
        State = state;
        PowerSource = powerSource;
    }
}

/// <summary>
/// Geolocation service interface
/// </summary>
public interface IGeolocationService
{
    /// <summary>
    /// Gets the last known location
    /// </summary>
    Task<Location?> GetLastKnownLocationAsync();

    /// <summary>
    /// Gets the current location
    /// </summary>
    /// <param name="request">Geolocation request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Location?> GetLocationAsync(GeolocationRequest? request = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if location services are enabled
    /// </summary>
    Task<bool> IsLocationEnabledAsync();
}

/// <summary>
/// Geolocation request parameters
/// </summary>
public class GeolocationRequest
{
    /// <summary>
    /// Gets or sets the desired accuracy
    /// </summary>
    public GeolocationAccuracy DesiredAccuracy { get; set; } = GeolocationAccuracy.Medium;

    /// <summary>
    /// Gets or sets the timeout for the request
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}

/// <summary>
/// Geolocation accuracy enumeration
/// </summary>
public enum GeolocationAccuracy
{
    /// <summary>
    /// Low accuracy (within 3000 meters)
    /// </summary>
    Low,

    /// <summary>
    /// Medium accuracy (within 100 meters)
    /// </summary>
    Medium,

    /// <summary>
    /// High accuracy (within 10 meters)
    /// </summary>
    High,

    /// <summary>
    /// Best accuracy available
    /// </summary>
    Best
}

/// <summary>
/// Location information
/// </summary>
public class Location
{
    /// <summary>
    /// Gets or sets the latitude
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the altitude in meters
    /// </summary>
    public double? Altitude { get; set; }

    /// <summary>
    /// Gets or sets the accuracy in meters
    /// </summary>
    public double? Accuracy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the speed in meters per second
    /// </summary>
    public double? Speed { get; set; }

    /// <summary>
    /// Gets or sets the course (direction of travel) in degrees
    /// </summary>
    public double? Course { get; set; }
}

/// <summary>
/// Device orientation service interface
/// </summary>
public interface IOrientationService
{
    /// <summary>
    /// Starts monitoring device orientation
    /// </summary>
    void Start(SensorSpeed speed = SensorSpeed.UI);

    /// <summary>
    /// Stops monitoring device orientation
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets whether orientation monitoring is active
    /// </summary>
    bool IsMonitoring { get; }

    /// <summary>
    /// Event raised when orientation changes
    /// </summary>
    event EventHandler<OrientationChangedEventArgs> OrientationChanged;
}

/// <summary>
/// Event arguments for orientation changes
/// </summary>
public class OrientationChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the quaternion representing the orientation
    /// </summary>
    public Quaternion Orientation { get; }

    /// <summary>
    /// Initializes a new instance of the OrientationChangedEventArgs class
    /// </summary>
    public OrientationChangedEventArgs(Quaternion orientation)
    {
        Orientation = orientation;
    }
}

/// <summary>
/// Quaternion for representing 3D orientation
/// </summary>
public struct Quaternion
{
    /// <summary>
    /// X component
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y component
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Z component
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// W component
    /// </summary>
    public double W { get; set; }
}

/// <summary>
/// Accelerometer service interface
/// </summary>
public interface IAccelerometerService
{
    /// <summary>
    /// Starts monitoring accelerometer
    /// </summary>
    void Start(SensorSpeed speed = SensorSpeed.UI);

    /// <summary>
    /// Stops monitoring accelerometer
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets whether accelerometer monitoring is active
    /// </summary>
    bool IsMonitoring { get; }

    /// <summary>
    /// Event raised when accelerometer reading changes
    /// </summary>
    event EventHandler<AccelerometerChangedEventArgs> ReadingChanged;
}

/// <summary>
/// Event arguments for accelerometer changes
/// </summary>
public class AccelerometerChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the acceleration vector
    /// </summary>
    public Vector3 Acceleration { get; }

    /// <summary>
    /// Initializes a new instance of the AccelerometerChangedEventArgs class
    /// </summary>
    public AccelerometerChangedEventArgs(Vector3 acceleration)
    {
        Acceleration = acceleration;
    }
}

/// <summary>
/// Gyroscope service interface
/// </summary>
public interface IGyroscopeService
{
    /// <summary>
    /// Starts monitoring gyroscope
    /// </summary>
    void Start(SensorSpeed speed = SensorSpeed.UI);

    /// <summary>
    /// Stops monitoring gyroscope
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets whether gyroscope monitoring is active
    /// </summary>
    bool IsMonitoring { get; }

    /// <summary>
    /// Event raised when gyroscope reading changes
    /// </summary>
    event EventHandler<GyroscopeChangedEventArgs> ReadingChanged;
}

/// <summary>
/// Event arguments for gyroscope changes
/// </summary>
public class GyroscopeChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the angular velocity vector
    /// </summary>
    public Vector3 AngularVelocity { get; }

    /// <summary>
    /// Initializes a new instance of the GyroscopeChangedEventArgs class
    /// </summary>
    public GyroscopeChangedEventArgs(Vector3 angularVelocity)
    {
        AngularVelocity = angularVelocity;
    }
}

/// <summary>
/// Magnetometer service interface
/// </summary>
public interface IMagnetometerService
{
    /// <summary>
    /// Starts monitoring magnetometer
    /// </summary>
    void Start(SensorSpeed speed = SensorSpeed.UI);

    /// <summary>
    /// Stops monitoring magnetometer
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets whether magnetometer monitoring is active
    /// </summary>
    bool IsMonitoring { get; }

    /// <summary>
    /// Event raised when magnetometer reading changes
    /// </summary>
    event EventHandler<MagnetometerChangedEventArgs> ReadingChanged;
}

/// <summary>
/// Event arguments for magnetometer changes
/// </summary>
public class MagnetometerChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the magnetic field vector
    /// </summary>
    public Vector3 MagneticField { get; }

    /// <summary>
    /// Initializes a new instance of the MagnetometerChangedEventArgs class
    /// </summary>
    public MagnetometerChangedEventArgs(Vector3 magneticField)
    {
        MagneticField = magneticField;
    }
}

/// <summary>
/// 3D vector structure
/// </summary>
public struct Vector3
{
    /// <summary>
    /// X component
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y component
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Z component
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Initializes a new instance of the Vector3 struct
    /// </summary>
    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}

/// <summary>
/// Sensor speed enumeration
/// </summary>
public enum SensorSpeed
{
    /// <summary>
    /// Default speed (varies by platform)
    /// </summary>
    Default,

    /// <summary>
    /// Suitable for UI updates
    /// </summary>
    UI,

    /// <summary>
    /// Game update speed
    /// </summary>
    Game,

    /// <summary>
    /// Fastest speed available
    /// </summary>
    Fastest
}
