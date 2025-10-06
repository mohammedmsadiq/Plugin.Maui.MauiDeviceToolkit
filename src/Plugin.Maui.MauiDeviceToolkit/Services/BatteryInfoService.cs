using MauiBattery = Microsoft.Maui.Devices.Battery;
using MauiBatteryState = Microsoft.Maui.Devices.BatteryState;
using MauiBatteryPowerSource = Microsoft.Maui.Devices.BatteryPowerSource;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IBatteryInfo using MAUI Essentials
/// </summary>
public class BatteryInfoService : Interfaces.IBatteryInfo
{
    /// <inheritdoc/>
    public double ChargeLevel => MauiBattery.Default.ChargeLevel;

    /// <inheritdoc/>
    public Interfaces.BatteryState State => MapBatteryState(MauiBattery.Default.State);

    /// <inheritdoc/>
    public Interfaces.BatteryPowerSource PowerSource => MapPowerSource(MauiBattery.Default.PowerSource);

    /// <inheritdoc/>
    public event EventHandler<Interfaces.BatteryInfoChangedEventArgs>? BatteryInfoChanged;

    /// <summary>
    /// Initializes a new instance of the BatteryInfoService class
    /// </summary>
    public BatteryInfoService()
    {
        MauiBattery.Default.BatteryInfoChanged += OnBatteryInfoChanged;
    }

    private void OnBatteryInfoChanged(object? sender, Microsoft.Maui.Devices.BatteryInfoChangedEventArgs e)
    {
        var args = new Interfaces.BatteryInfoChangedEventArgs(
            e.ChargeLevel,
            MapBatteryState(e.State),
            MapPowerSource(e.PowerSource));

        BatteryInfoChanged?.Invoke(this, args);
    }

    private static Interfaces.BatteryState MapBatteryState(MauiBatteryState state)
    {
        return state switch
        {
            MauiBatteryState.Unknown => Interfaces.BatteryState.Unknown,
            MauiBatteryState.Charging => Interfaces.BatteryState.Charging,
            MauiBatteryState.Discharging => Interfaces.BatteryState.Discharging,
            MauiBatteryState.Full => Interfaces.BatteryState.Full,
            MauiBatteryState.NotCharging => Interfaces.BatteryState.NotCharging,
            MauiBatteryState.NotPresent => Interfaces.BatteryState.NotPresent,
            _ => Interfaces.BatteryState.Unknown
        };
    }

    private static Interfaces.BatteryPowerSource MapPowerSource(MauiBatteryPowerSource source)
    {
        return source switch
        {
            MauiBatteryPowerSource.Unknown => Interfaces.BatteryPowerSource.Unknown,
            MauiBatteryPowerSource.Battery => Interfaces.BatteryPowerSource.Battery,
            MauiBatteryPowerSource.AC => Interfaces.BatteryPowerSource.AC,
            MauiBatteryPowerSource.Usb => Interfaces.BatteryPowerSource.Usb,
            MauiBatteryPowerSource.Wireless => Interfaces.BatteryPowerSource.Wireless,
            _ => Interfaces.BatteryPowerSource.Unknown
        };
    }
}
