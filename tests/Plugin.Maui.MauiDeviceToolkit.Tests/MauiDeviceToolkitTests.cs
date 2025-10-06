using Plugin.Maui.MauiDeviceToolkit.Interfaces;
using Xunit;

namespace Plugin.Maui.MauiDeviceToolkit.Tests;

public class MauiDeviceToolkitTests
{
    [Fact]
    public void Permissions_ShouldNotBeNull()
    {
        // Act
        var service = MauiDeviceToolkit.Permissions;

        // Assert
        Assert.NotNull(service);
        Assert.IsAssignableFrom<IPermissionService>(service);
    }

    [Fact]
    public void DeviceInfo_ShouldNotBeNull()
    {
        // Act
        var service = MauiDeviceToolkit.DeviceInfo;

        // Assert
        Assert.NotNull(service);
        Assert.IsAssignableFrom<IDeviceInfoService>(service);
    }

    [Fact]
    public void Network_ShouldNotBeNull()
    {
        // Act
        var service = MauiDeviceToolkit.Network;

        // Assert
        Assert.NotNull(service);
        Assert.IsAssignableFrom<INetworkService>(service);
    }

    [Fact]
    public void Sensors_ShouldNotBeNull()
    {
        // Act
        var service = MauiDeviceToolkit.Sensors;

        // Assert
        Assert.NotNull(service);
        Assert.IsAssignableFrom<ISensorService>(service);
    }

    [Fact]
    public void Configure_ShouldAllowCustomServices()
    {
        // Arrange
        var customPermissions = new MockPermissionService();
        var customDeviceInfo = new MockDeviceInfoService();
        var customNetwork = new MockNetworkService();
        var customSensors = new MockSensorService();

        // Act
        MauiDeviceToolkit.Configure(
            permissionService: customPermissions,
            deviceInfoService: customDeviceInfo,
            networkService: customNetwork,
            sensorService: customSensors);

        // Assert
        Assert.Same(customPermissions, MauiDeviceToolkit.Permissions);
        Assert.Same(customDeviceInfo, MauiDeviceToolkit.DeviceInfo);
        Assert.Same(customNetwork, MauiDeviceToolkit.Network);
        Assert.Same(customSensors, MauiDeviceToolkit.Sensors);

        // Cleanup
        MauiDeviceToolkit.Reset();
    }

    [Fact]
    public void Reset_ShouldClearCustomServices()
    {
        // Arrange
        var customPermissions = new MockPermissionService();
        MauiDeviceToolkit.Configure(permissionService: customPermissions);

        // Act
        MauiDeviceToolkit.Reset();

        // Assert
        Assert.NotSame(customPermissions, MauiDeviceToolkit.Permissions);
    }
}

// Mock implementations for testing
internal class MockPermissionService : IPermissionService
{
    public Task<Interfaces.PermissionStatus> CheckPermissionAsync<TPermission>() where TPermission : Microsoft.Maui.ApplicationModel.Permissions.BasePermission, new()
        => Task.FromResult(Interfaces.PermissionStatus.Granted);

    public Task<Interfaces.PermissionStatus> RequestPermissionAsync<TPermission>() where TPermission : Microsoft.Maui.ApplicationModel.Permissions.BasePermission, new()
        => Task.FromResult(Interfaces.PermissionStatus.Granted);

    public void OpenAppSettings() { }
}

internal class MockDeviceInfoService : IDeviceInfoService
{
    public string Model => "Test Model";
    public string Manufacturer => "Test Manufacturer";
    public string Name => "Test Device";
    public string VersionString => "1.0.0";
    public Interfaces.DevicePlatform Platform => Interfaces.DevicePlatform.Android;
    public Interfaces.DeviceIdiom Idiom => Interfaces.DeviceIdiom.Phone;
    public Interfaces.DeviceType DeviceType => Interfaces.DeviceType.Virtual;
}

internal class MockNetworkService : INetworkService
{
    public Interfaces.NetworkAccess NetworkAccess => Interfaces.NetworkAccess.Internet;
    public IEnumerable<Interfaces.ConnectionProfile> ConnectionProfiles => new[] { Interfaces.ConnectionProfile.WiFi };
    public bool IsConnected => true;
    public event EventHandler<Interfaces.ConnectivityChangedEventArgs>? ConnectivityChanged;
}

internal class MockSensorService : ISensorService
{
    public IBatteryInfo Battery => new MockBatteryInfo();
    public IGeolocationService Geolocation => new MockGeolocationService();
    public IOrientationService Orientation => new MockOrientationService();
    public IAccelerometerService Accelerometer => new MockAccelerometerService();
    public IGyroscopeService Gyroscope => new MockGyroscopeService();
    public IMagnetometerService Magnetometer => new MockMagnetometerService();
}

internal class MockBatteryInfo : IBatteryInfo
{
    public double ChargeLevel => 0.75;
    public Interfaces.BatteryState State => Interfaces.BatteryState.Charging;
    public Interfaces.BatteryPowerSource PowerSource => Interfaces.BatteryPowerSource.AC;
    public event EventHandler<Interfaces.BatteryInfoChangedEventArgs>? BatteryInfoChanged;
}

internal class MockGeolocationService : IGeolocationService
{
    public Task<Interfaces.Location?> GetLastKnownLocationAsync() => Task.FromResult<Interfaces.Location?>(null);
    public Task<Interfaces.Location?> GetLocationAsync(Interfaces.GeolocationRequest? request = null, CancellationToken cancellationToken = default) 
        => Task.FromResult<Interfaces.Location?>(null);
    public Task<bool> IsLocationEnabledAsync() => Task.FromResult(true);
}

internal class MockOrientationService : IOrientationService
{
    public bool IsMonitoring => false;
    public event EventHandler<Interfaces.OrientationChangedEventArgs>? OrientationChanged;
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI) { }
    public void Stop() { }
}

internal class MockAccelerometerService : IAccelerometerService
{
    public bool IsMonitoring => false;
    public event EventHandler<Interfaces.AccelerometerChangedEventArgs>? ReadingChanged;
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI) { }
    public void Stop() { }
}

internal class MockGyroscopeService : IGyroscopeService
{
    public bool IsMonitoring => false;
    public event EventHandler<Interfaces.GyroscopeChangedEventArgs>? ReadingChanged;
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI) { }
    public void Stop() { }
}

internal class MockMagnetometerService : IMagnetometerService
{
    public bool IsMonitoring => false;
    public event EventHandler<Interfaces.MagnetometerChangedEventArgs>? ReadingChanged;
    public void Start(Interfaces.SensorSpeed speed = Interfaces.SensorSpeed.UI) { }
    public void Stop() { }
}
