# Plugin.Maui.MauiDeviceToolkit - Usage Examples

## Table of Contents
- [Permission Examples](#permission-examples)
- [Device Information Examples](#device-information-examples)
- [Network Monitoring Examples](#network-monitoring-examples)
- [Sensor Examples](#sensor-examples)

## Permission Examples

### Example 1: Request Camera Permission

```csharp
using Plugin.Maui.MauiDeviceToolkit;
using Microsoft.Maui.ApplicationModel;

public async Task<bool> RequestCameraAccessAsync()
{
    // Check current status first
    var status = await MauiDeviceToolkit.Permissions
        .CheckPermissionAsync<Permissions.Camera>();
    
    if (status == Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted)
    {
        return true;
    }
    
    // Request permission
    var result = await MauiDeviceToolkit.Permissions
        .RequestPermissionAsync<Permissions.Camera>();
    
    if (result != Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted)
    {
        // Permission denied - guide user to settings
        await App.Current.MainPage.DisplayAlert(
            "Permission Required",
            "Camera permission is required. Please enable it in settings.",
            "Go to Settings",
            "Cancel"
        );
        
        if (await App.Current.MainPage.DisplayAlert(...))
        {
            MauiDeviceToolkit.Permissions.OpenAppSettings();
        }
    }
    
    return result == Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted;
}
```

### Example 2: Request Location Permission

```csharp
public async Task<bool> RequestLocationPermissionAsync()
{
    var status = await MauiDeviceToolkit.Permissions
        .RequestPermissionAsync<Permissions.LocationWhenInUse>();
    
    return status == Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted;
}
```

## Device Information Examples

### Example 3: Display Device Details

```csharp
public void ShowDeviceInfo()
{
    var device = MauiDeviceToolkit.DeviceInfo;
    
    var info = $@"
Device Information:
- Manufacturer: {device.Manufacturer}
- Model: {device.Model}
- Name: {device.Name}
- Platform: {device.Platform}
- OS Version: {device.VersionString}
- Device Type: {device.Idiom}
- Is Virtual: {device.DeviceType == Plugin.Maui.MauiDeviceToolkit.Interfaces.DeviceType.Virtual}
";
    
    Console.WriteLine(info);
}
```

### Example 4: Platform-Specific Logic

```csharp
public void ConfigureForPlatform()
{
    var platform = MauiDeviceToolkit.DeviceInfo.Platform;
    
    switch (platform)
    {
        case Plugin.Maui.MauiDeviceToolkit.Interfaces.DevicePlatform.Android:
            // Android-specific configuration
            break;
            
        case Plugin.Maui.MauiDeviceToolkit.Interfaces.DevicePlatform.iOS:
            // iOS-specific configuration
            break;
            
        case Plugin.Maui.MauiDeviceToolkit.Interfaces.DevicePlatform.Windows:
            // Windows-specific configuration
            break;
    }
}
```

## Network Monitoring Examples

### Example 5: Check Internet Connectivity

```csharp
public bool IsInternetAvailable()
{
    var network = MauiDeviceToolkit.Network;
    
    if (!network.IsConnected)
    {
        return false;
    }
    
    // Check for full internet access
    return network.NetworkAccess == 
        Plugin.Maui.MauiDeviceToolkit.Interfaces.NetworkAccess.Internet;
}
```

### Example 6: Monitor Network Changes

```csharp
public class NetworkMonitor : IDisposable
{
    public NetworkMonitor()
    {
        MauiDeviceToolkit.Network.ConnectivityChanged += OnConnectivityChanged;
    }
    
    private void OnConnectivityChanged(object sender, 
        Plugin.Maui.MauiDeviceToolkit.Interfaces.ConnectivityChangedEventArgs e)
    {
        var status = e.NetworkAccess switch
        {
            Plugin.Maui.MauiDeviceToolkit.Interfaces.NetworkAccess.Internet => 
                "Connected to Internet",
            Plugin.Maui.MauiDeviceToolkit.Interfaces.NetworkAccess.Local => 
                "Connected to Local Network",
            Plugin.Maui.MauiDeviceToolkit.Interfaces.NetworkAccess.None => 
                "No Connection",
            _ => "Unknown"
        };
        
        var profiles = string.Join(", ", e.ConnectionProfiles
            .Select(p => p.ToString()));
        
        Console.WriteLine($"Network Status: {status}");
        Console.WriteLine($"Connection Types: {profiles}");
    }
    
    public void Dispose()
    {
        MauiDeviceToolkit.Network.ConnectivityChanged -= OnConnectivityChanged;
    }
}
```

### Example 7: Connection Type Detection

```csharp
public string GetConnectionType()
{
    var profiles = MauiDeviceToolkit.Network.ConnectionProfiles;
    
    if (profiles.Contains(Plugin.Maui.MauiDeviceToolkit.Interfaces.ConnectionProfile.WiFi))
    {
        return "WiFi";
    }
    else if (profiles.Contains(Plugin.Maui.MauiDeviceToolkit.Interfaces.ConnectionProfile.Cellular))
    {
        return "Cellular";
    }
    else if (profiles.Contains(Plugin.Maui.MauiDeviceToolkit.Interfaces.ConnectionProfile.Ethernet))
    {
        return "Ethernet";
    }
    
    return "Unknown";
}
```

## Sensor Examples

### Example 8: Monitor Battery Status

```csharp
public class BatteryMonitor : IDisposable
{
    public BatteryMonitor()
    {
        var battery = MauiDeviceToolkit.Sensors.Battery;
        
        // Display current status
        DisplayBatteryStatus(battery);
        
        // Subscribe to changes
        battery.BatteryInfoChanged += OnBatteryInfoChanged;
    }
    
    private void DisplayBatteryStatus(
        Plugin.Maui.MauiDeviceToolkit.Interfaces.IBatteryInfo battery)
    {
        var percentage = (int)(battery.ChargeLevel * 100);
        Console.WriteLine($"Battery: {percentage}%");
        Console.WriteLine($"State: {battery.State}");
        Console.WriteLine($"Power Source: {battery.PowerSource}");
    }
    
    private void OnBatteryInfoChanged(object sender, 
        Plugin.Maui.MauiDeviceToolkit.Interfaces.BatteryInfoChangedEventArgs e)
    {
        if (e.ChargeLevel < 0.2 && 
            e.State == Plugin.Maui.MauiDeviceToolkit.Interfaces.BatteryState.Discharging)
        {
            // Alert user about low battery
            Console.WriteLine("⚠️ Low battery warning!");
        }
    }
    
    public void Dispose()
    {
        MauiDeviceToolkit.Sensors.Battery.BatteryInfoChanged -= OnBatteryInfoChanged;
    }
}
```

### Example 9: Get Current Location

```csharp
public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
{
    // Check permission first
    var hasPermission = await MauiDeviceToolkit.Permissions
        .CheckPermissionAsync<Permissions.LocationWhenInUse>();
        
    if (hasPermission != Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted)
    {
        hasPermission = await MauiDeviceToolkit.Permissions
            .RequestPermissionAsync<Permissions.LocationWhenInUse>();
            
        if (hasPermission != Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted)
        {
            return null;
        }
    }
    
    // Get location with high accuracy
    var request = new Plugin.Maui.MauiDeviceToolkit.Interfaces.GeolocationRequest
    {
        DesiredAccuracy = Plugin.Maui.MauiDeviceToolkit.Interfaces.GeolocationAccuracy.High,
        Timeout = TimeSpan.FromSeconds(10)
    };
    
    var location = await MauiDeviceToolkit.Sensors.Geolocation
        .GetLocationAsync(request);
    
    if (location != null)
    {
        return (location.Latitude, location.Longitude);
    }
    
    // Try last known location as fallback
    var lastKnown = await MauiDeviceToolkit.Sensors.Geolocation
        .GetLastKnownLocationAsync();
        
    if (lastKnown != null)
    {
        return (lastKnown.Latitude, lastKnown.Longitude);
    }
    
    return null;
}
```

### Example 10: Monitor Device Motion

```csharp
public class MotionDetector : IDisposable
{
    private const double ShakeThreshold = 3.0;
    
    public event EventHandler DeviceShaken;
    
    public void StartMonitoring()
    {
        var accelerometer = MauiDeviceToolkit.Sensors.Accelerometer;
        accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
        accelerometer.Start(Plugin.Maui.MauiDeviceToolkit.Interfaces.SensorSpeed.UI);
    }
    
    private void OnAccelerometerReadingChanged(object sender, 
        Plugin.Maui.MauiDeviceToolkit.Interfaces.AccelerometerChangedEventArgs e)
    {
        var acceleration = e.Acceleration;
        var magnitude = Math.Sqrt(
            acceleration.X * acceleration.X +
            acceleration.Y * acceleration.Y +
            acceleration.Z * acceleration.Z
        );
        
        if (magnitude > ShakeThreshold)
        {
            DeviceShaken?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void StopMonitoring()
    {
        var accelerometer = MauiDeviceToolkit.Sensors.Accelerometer;
        accelerometer.Stop();
        accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
    }
    
    public void Dispose()
    {
        StopMonitoring();
    }
}
```

### Example 11: Compass Heading

```csharp
public class CompassService : IDisposable
{
    public event EventHandler<double> HeadingChanged;
    
    public void Start()
    {
        var magnetometer = MauiDeviceToolkit.Sensors.Magnetometer;
        magnetometer.ReadingChanged += OnMagnetometerReadingChanged;
        magnetometer.Start(Plugin.Maui.MauiDeviceToolkit.Interfaces.SensorSpeed.UI);
    }
    
    private void OnMagnetometerReadingChanged(object sender, 
        Plugin.Maui.MauiDeviceToolkit.Interfaces.MagnetometerChangedEventArgs e)
    {
        // Calculate compass heading from magnetic field
        var heading = Math.Atan2(e.MagneticField.Y, e.MagneticField.X) * (180 / Math.PI);
        
        if (heading < 0)
        {
            heading += 360;
        }
        
        HeadingChanged?.Invoke(this, heading);
    }
    
    public void Stop()
    {
        var magnetometer = MauiDeviceToolkit.Sensors.Magnetometer;
        magnetometer.Stop();
        magnetometer.ReadingChanged -= OnMagnetometerReadingChanged;
    }
    
    public void Dispose()
    {
        Stop();
    }
}
```

## Complete MAUI Page Example

### Example 12: Device Info Page

```csharp
using Plugin.Maui.MauiDeviceToolkit;

public partial class DeviceInfoPage : ContentPage
{
    public DeviceInfoPage()
    {
        InitializeComponent();
        LoadDeviceInfo();
        MonitorNetwork();
        MonitorBattery();
    }
    
    private void LoadDeviceInfo()
    {
        var device = MauiDeviceToolkit.DeviceInfo;
        
        DeviceLabel.Text = $"{device.Manufacturer} {device.Model}";
        PlatformLabel.Text = $"{device.Platform} {device.VersionString}";
        TypeLabel.Text = device.Idiom.ToString();
    }
    
    private void MonitorNetwork()
    {
        UpdateNetworkStatus();
        MauiDeviceToolkit.Network.ConnectivityChanged += (s, e) => 
        {
            MainThread.BeginInvokeOnMainThread(UpdateNetworkStatus);
        };
    }
    
    private void UpdateNetworkStatus()
    {
        var network = MauiDeviceToolkit.Network;
        NetworkLabel.Text = network.IsConnected ? "Connected" : "Disconnected";
        ConnectionTypeLabel.Text = string.Join(", ", network.ConnectionProfiles);
    }
    
    private void MonitorBattery()
    {
        UpdateBatteryStatus();
        MauiDeviceToolkit.Sensors.Battery.BatteryInfoChanged += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(UpdateBatteryStatus);
        };
    }
    
    private void UpdateBatteryStatus()
    {
        var battery = MauiDeviceToolkit.Sensors.Battery;
        var percentage = (int)(battery.ChargeLevel * 100);
        BatteryLabel.Text = $"{percentage}% - {battery.State}";
    }
}
```

## Testing Examples

### Example 13: Unit Testing with Mocks

```csharp
using Plugin.Maui.MauiDeviceToolkit;
using Plugin.Maui.MauiDeviceToolkit.Interfaces;

[Fact]
public void TestWithMockServices()
{
    // Arrange
    var mockNetwork = new MockNetworkService();
    MauiDeviceToolkit.Configure(networkService: mockNetwork);
    
    // Act
    var isConnected = MauiDeviceToolkit.Network.IsConnected;
    
    // Assert
    Assert.True(isConnected);
    
    // Cleanup
    MauiDeviceToolkit.Reset();
}
```

---

For more examples and detailed API documentation, see the [README.md](../README.md) file.
