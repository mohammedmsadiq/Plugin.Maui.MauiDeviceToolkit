using MauiDeviceInfo = Microsoft.Maui.Devices.DeviceInfo;
using MauiDevicePlatform = Microsoft.Maui.Devices.DevicePlatform;
using MauiDeviceIdiom = Microsoft.Maui.Devices.DeviceIdiom;
using MauiDeviceType = Microsoft.Maui.Devices.DeviceType;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IDeviceInfoService using MAUI Essentials
/// </summary>
public class DeviceInfoService : Interfaces.IDeviceInfoService
{
    /// <inheritdoc/>
    public string Model => MauiDeviceInfo.Current.Model;

    /// <inheritdoc/>
    public string Manufacturer => MauiDeviceInfo.Current.Manufacturer;

    /// <inheritdoc/>
    public string Name => MauiDeviceInfo.Current.Name;

    /// <inheritdoc/>
    public string VersionString => MauiDeviceInfo.Current.VersionString;

    /// <inheritdoc/>
    public Interfaces.DevicePlatform Platform => MapPlatform(MauiDeviceInfo.Current.Platform);

    /// <inheritdoc/>
    public Interfaces.DeviceIdiom Idiom => MapIdiom(MauiDeviceInfo.Current.Idiom);

    /// <inheritdoc/>
    public Interfaces.DeviceType DeviceType => MapDeviceType(MauiDeviceInfo.Current.DeviceType);

    private static Interfaces.DevicePlatform MapPlatform(MauiDevicePlatform platform)
    {
        if (platform == MauiDevicePlatform.Android)
            return Interfaces.DevicePlatform.Android;
        if (platform == MauiDevicePlatform.iOS)
            return Interfaces.DevicePlatform.iOS;
        if (platform == MauiDevicePlatform.MacCatalyst)
            return Interfaces.DevicePlatform.MacCatalyst;
        if (platform == MauiDevicePlatform.WinUI)
            return Interfaces.DevicePlatform.Windows;
        if (platform == MauiDevicePlatform.Tizen)
            return Interfaces.DevicePlatform.Tizen;

        return Interfaces.DevicePlatform.Unknown;
    }

    private static Interfaces.DeviceIdiom MapIdiom(MauiDeviceIdiom idiom)
    {
        if (idiom == MauiDeviceIdiom.Phone)
            return Interfaces.DeviceIdiom.Phone;
        if (idiom == MauiDeviceIdiom.Tablet)
            return Interfaces.DeviceIdiom.Tablet;
        if (idiom == MauiDeviceIdiom.Desktop)
            return Interfaces.DeviceIdiom.Desktop;
        if (idiom == MauiDeviceIdiom.TV)
            return Interfaces.DeviceIdiom.TV;
        if (idiom == MauiDeviceIdiom.Watch)
            return Interfaces.DeviceIdiom.Watch;

        return Interfaces.DeviceIdiom.Unknown;
    }

    private static Interfaces.DeviceType MapDeviceType(MauiDeviceType deviceType)
    {
        return deviceType switch
        {
            MauiDeviceType.Physical => Interfaces.DeviceType.Physical,
            MauiDeviceType.Virtual => Interfaces.DeviceType.Virtual,
            _ => Interfaces.DeviceType.Unknown
        };
    }
}
