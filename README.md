# Plugin.Maui.MauiDeviceToolkit

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.MauiDeviceToolkit.svg)](https://www.nuget.org/packages/Plugin.Maui.MauiDeviceToolkit/)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/download)
[![MAUI](https://img.shields.io/badge/MAUI-Supported-blue)](https://dotnet.microsoft.com/apps/maui)

A comprehensive .NET MAUI plugin providing centralized helpers for **permissions**, **device information**, **network connectivity**, and **sensors** (Camera, GPS, Battery, Orientation, and more).

🎯 **Goal**: Replace multiple dependencies like `Plugin.Permissions` + `Plugin.DeviceInfo` + `Essentials` with a single, unified API.

## Features

### 🔐 Permission Management
- Check and request runtime permissions
- Support for all common permission types
- Cross-platform API (Android, iOS, Windows, macOS)
- Direct access to app settings

### 📱 Device Information
- Device model, manufacturer, and name
- Operating system version
- Platform detection (Android, iOS, Windows, macOS)
- Device type (phone, tablet, desktop, TV, watch)
- Physical vs. virtual device detection

### 🌐 Network Connectivity
- Real-time network status monitoring
- Connection type detection (WiFi, Cellular, Ethernet, Bluetooth)
- Internet connectivity checking
- Network change events

### 📡 Sensor Access
- **Battery**: Charge level, charging state, power source
- **Geolocation**: GPS location, last known location
- **Orientation**: Device orientation (quaternion)
- **Accelerometer**: Motion and acceleration data
- **Gyroscope**: Rotation and angular velocity
- **Magnetometer**: Compass and magnetic field data

## Installation

Install the NuGet package:

```bash
dotnet add package Plugin.Maui.MauiDeviceToolkit
```

Or via Package Manager:

```bash
Install-Package Plugin.Maui.MauiDeviceToolkit
```

## Quick Start

### 1. Basic Usage

```csharp
using Plugin.Maui.MauiDeviceToolkit;

// Check device information
var deviceInfo = MauiDeviceToolkit.DeviceInfo;
Console.WriteLine($"Device: {deviceInfo.Manufacturer} {deviceInfo.Model}");
Console.WriteLine($"Platform: {deviceInfo.Platform}");
Console.WriteLine($"OS Version: {deviceInfo.VersionString}");

// Check network connectivity
var network = MauiDeviceToolkit.Network;
if (network.IsConnected)
{
    Console.WriteLine($"Connected via: {string.Join(", ", network.ConnectionProfiles)}");
}

// Get battery information
var battery = MauiDeviceToolkit.Sensors.Battery;
Console.WriteLine($"Battery: {battery.ChargeLevel * 100}% - {battery.State}");
```

### 2. Requesting Permissions

```csharp
using Microsoft.Maui.ApplicationModel;

// Request location permission
var status = await MauiDeviceToolkit.Permissions.RequestPermissionAsync<Permissions.LocationWhenInUse>();

if (status == Plugin.Maui.MauiDeviceToolkit.Interfaces.PermissionStatus.Granted)
{
    // Get current location
    var location = await MauiDeviceToolkit.Sensors.Geolocation.GetLocationAsync();
    if (location != null)
    {
        Console.WriteLine($"Location: {location.Latitude}, {location.Longitude}");
    }
}
else
{
    // Open settings if permission denied
    MauiDeviceToolkit.Permissions.OpenAppSettings();
}
```

### 3. Monitoring Network Changes

```csharp
// Subscribe to connectivity changes
MauiDeviceToolkit.Network.ConnectivityChanged += (sender, e) =>
{
    Console.WriteLine($"Network status changed: {e.NetworkAccess}");
    Console.WriteLine($"Profiles: {string.Join(", ", e.ConnectionProfiles)}");
};
```

### 4. Using Sensors

```csharp
// Start accelerometer
var accelerometer = MauiDeviceToolkit.Sensors.Accelerometer;
accelerometer.ReadingChanged += (sender, e) =>
{
    Console.WriteLine($"Acceleration: X={e.Acceleration.X}, Y={e.Acceleration.Y}, Z={e.Acceleration.Z}");
};
accelerometer.Start(Plugin.Maui.MauiDeviceToolkit.Interfaces.SensorSpeed.UI);

// Later, stop monitoring
accelerometer.Stop();
```

## API Reference

### MauiDeviceToolkit Static Class

The main entry point for accessing all services:

- **`MauiDeviceToolkit.Permissions`** - Permission service
- **`MauiDeviceToolkit.DeviceInfo`** - Device information service
- **`MauiDeviceToolkit.Network`** - Network connectivity service  
- **`MauiDeviceToolkit.Sensors`** - Sensor services container

### Permission Service

```csharp
// Check permission status
var status = await MauiDeviceToolkit.Permissions.CheckPermissionAsync<Permissions.Camera>();

// Request permission
var result = await MauiDeviceToolkit.Permissions.RequestPermissionAsync<Permissions.Camera>();

// Open app settings
MauiDeviceToolkit.Permissions.OpenAppSettings();
```

### Device Information

```csharp
var info = MauiDeviceToolkit.DeviceInfo;
string model = info.Model;              // "iPhone 14 Pro"
string manufacturer = info.Manufacturer; // "Apple"
string name = info.Name;                 // "John's iPhone"
string version = info.VersionString;     // "16.0"
DevicePlatform platform = info.Platform; // iOS, Android, Windows, etc.
DeviceIdiom idiom = info.Idiom;         // Phone, Tablet, Desktop, etc.
DeviceType type = info.DeviceType;      // Physical, Virtual
```

### Network Service

```csharp
var network = MauiDeviceToolkit.Network;
bool connected = network.IsConnected;
NetworkAccess access = network.NetworkAccess;
var profiles = network.ConnectionProfiles;

// Event subscription
network.ConnectivityChanged += OnConnectivityChanged;
```

### Sensor Services

#### Battery

```csharp
var battery = MauiDeviceToolkit.Sensors.Battery;
double level = battery.ChargeLevel;           // 0.0 to 1.0
BatteryState state = battery.State;           // Charging, Discharging, Full, etc.
BatteryPowerSource source = battery.PowerSource; // Battery, AC, USB, Wireless

battery.BatteryInfoChanged += OnBatteryChanged;
```

#### Geolocation

```csharp
var geo = MauiDeviceToolkit.Sensors.Geolocation;

// Get last known location
var lastLocation = await geo.GetLastKnownLocationAsync();

// Get current location with custom accuracy
var request = new GeolocationRequest 
{
    DesiredAccuracy = GeolocationAccuracy.High,
    Timeout = TimeSpan.FromSeconds(10)
};
var location = await geo.GetLocationAsync(request);
```

#### Accelerometer, Gyroscope, Magnetometer, Orientation

```csharp
var sensor = MauiDeviceToolkit.Sensors.Accelerometer; // or Gyroscope, Magnetometer, Orientation

sensor.ReadingChanged += (s, e) => { /* handle reading */ };
sensor.Start(SensorSpeed.UI);
sensor.Stop();
```

## Platform Support

| Platform | Supported | Notes |
|----------|-----------|-------|
| Android  | ✅ Yes     | API 21+ |
| iOS      | ✅ Yes     | iOS 11+ |
| Windows  | ✅ Yes     | Windows 10.0.17763+ |
| macOS    | ✅ Yes     | macOS 13.1+ (Catalyst) |
| Tizen    | ⚠️ Partial | Limited testing |

## Advanced Usage

### Custom Service Implementations

You can provide custom implementations for testing or specialized behavior:

```csharp
// Create custom implementations
var mockPermissions = new MyCustomPermissionService();
var mockDeviceInfo = new MyCustomDeviceInfoService();

// Configure
MauiDeviceToolkit.Configure(
    permissionService: mockPermissions,
    deviceInfoService: mockDeviceInfo
);

// Reset to defaults
MauiDeviceToolkit.Reset();
```

## Requirements

- .NET 8.0 or higher
- .NET MAUI 8.0 or higher
- Platform-specific requirements per OS

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Authors

- Mohammed Sadiq

## Acknowledgments

This plugin wraps and simplifies the existing .NET MAUI Essentials APIs to provide a more unified and developer-friendly experience.
