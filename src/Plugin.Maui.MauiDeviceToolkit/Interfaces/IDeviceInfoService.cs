namespace Plugin.Maui.MauiDeviceToolkit.Interfaces;

/// <summary>
/// Service for retrieving device information
/// </summary>
public interface IDeviceInfoService
{
    /// <summary>
    /// Gets the device model (e.g., "iPhone 14 Pro", "Samsung Galaxy S23")
    /// </summary>
    string Model { get; }

    /// <summary>
    /// Gets the device manufacturer (e.g., "Apple", "Samsung")
    /// </summary>
    string Manufacturer { get; }

    /// <summary>
    /// Gets the device name set by the user
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the version of the operating system
    /// </summary>
    string VersionString { get; }

    /// <summary>
    /// Gets the platform the app is running on
    /// </summary>
    DevicePlatform Platform { get; }

    /// <summary>
    /// Gets the device idiom (phone, tablet, desktop, TV, watch, etc.)
    /// </summary>
    DeviceIdiom Idiom { get; }

    /// <summary>
    /// Gets the device type (physical or virtual)
    /// </summary>
    DeviceType DeviceType { get; }
}

/// <summary>
/// Device platform enumeration
/// </summary>
public enum DevicePlatform
{
    /// <summary>
    /// Android platform
    /// </summary>
    Android,

    /// <summary>
    /// iOS platform
    /// </summary>
    iOS,

    /// <summary>
    /// Windows platform
    /// </summary>
    Windows,

    /// <summary>
    /// macOS platform (Catalyst)
    /// </summary>
    MacCatalyst,

    /// <summary>
    /// Tizen platform
    /// </summary>
    Tizen,

    /// <summary>
    /// Unknown platform
    /// </summary>
    Unknown
}

/// <summary>
/// Device idiom enumeration
/// </summary>
public enum DeviceIdiom
{
    /// <summary>
    /// Phone device
    /// </summary>
    Phone,

    /// <summary>
    /// Tablet device
    /// </summary>
    Tablet,

    /// <summary>
    /// Desktop device
    /// </summary>
    Desktop,

    /// <summary>
    /// TV device
    /// </summary>
    TV,

    /// <summary>
    /// Watch device
    /// </summary>
    Watch,

    /// <summary>
    /// Unknown device idiom
    /// </summary>
    Unknown
}

/// <summary>
/// Device type enumeration
/// </summary>
public enum DeviceType
{
    /// <summary>
    /// Physical device
    /// </summary>
    Physical,

    /// <summary>
    /// Virtual/emulator device
    /// </summary>
    Virtual,

    /// <summary>
    /// Unknown device type
    /// </summary>
    Unknown
}
