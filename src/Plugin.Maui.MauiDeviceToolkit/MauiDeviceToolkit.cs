using Plugin.Maui.MauiDeviceToolkit.Interfaces;
using Plugin.Maui.MauiDeviceToolkit.Services;

namespace Plugin.Maui.MauiDeviceToolkit;

/// <summary>
/// Main entry point for the MauiDeviceToolkit plugin
/// Provides centralized access to all device-related services
/// </summary>
public static class MauiDeviceToolkit
{
    private static IPermissionService? _permissionService;
    private static IDeviceInfoService? _deviceInfoService;
    private static INetworkService? _networkService;
    private static ISensorService? _sensorService;

    /// <summary>
    /// Gets the permission service for managing runtime permissions
    /// </summary>
    public static IPermissionService Permissions => _permissionService ??= new PermissionService();

    /// <summary>
    /// Gets the device information service
    /// </summary>
    public static IDeviceInfoService DeviceInfo => _deviceInfoService ??= new DeviceInfoService();

    /// <summary>
    /// Gets the network connectivity service
    /// </summary>
    public static INetworkService Network => _networkService ??= new NetworkService();

    /// <summary>
    /// Gets the sensor service for accessing device sensors
    /// </summary>
    public static ISensorService Sensors => _sensorService ??= new SensorService();

    /// <summary>
    /// Configures custom service implementations (for testing or custom behavior)
    /// </summary>
    /// <param name="permissionService">Custom permission service implementation</param>
    /// <param name="deviceInfoService">Custom device info service implementation</param>
    /// <param name="networkService">Custom network service implementation</param>
    /// <param name="sensorService">Custom sensor service implementation</param>
    public static void Configure(
        IPermissionService? permissionService = null,
        IDeviceInfoService? deviceInfoService = null,
        INetworkService? networkService = null,
        ISensorService? sensorService = null)
    {
        if (permissionService != null)
            _permissionService = permissionService;

        if (deviceInfoService != null)
            _deviceInfoService = deviceInfoService;

        if (networkService != null)
            _networkService = networkService;

        if (sensorService != null)
            _sensorService = sensorService;
    }

    /// <summary>
    /// Resets all services to their default implementations
    /// </summary>
    public static void Reset()
    {
        _permissionService = null;
        _deviceInfoService = null;
        _networkService = null;
        _sensorService = null;
    }
}
