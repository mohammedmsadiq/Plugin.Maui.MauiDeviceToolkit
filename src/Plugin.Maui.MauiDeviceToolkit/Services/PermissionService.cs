using MauiPermissions = Microsoft.Maui.ApplicationModel.Permissions;
using MauiPermissionStatus = Microsoft.Maui.ApplicationModel.PermissionStatus;

namespace Plugin.Maui.MauiDeviceToolkit.Services;

/// <summary>
/// Default implementation of IPermissionService using MAUI Essentials
/// </summary>
public class PermissionService : Interfaces.IPermissionService
{
    /// <inheritdoc/>
    public async Task<Interfaces.PermissionStatus> CheckPermissionAsync<TPermission>() where TPermission : MauiPermissions.BasePermission, new()
    {
        var status = await MauiPermissions.CheckStatusAsync<TPermission>();
        return MapPermissionStatus(status);
    }

    /// <inheritdoc/>
    public async Task<Interfaces.PermissionStatus> RequestPermissionAsync<TPermission>() where TPermission : MauiPermissions.BasePermission, new()
    {
        var status = await MauiPermissions.RequestAsync<TPermission>();
        return MapPermissionStatus(status);
    }

    /// <inheritdoc/>
    public void OpenAppSettings()
    {
        Microsoft.Maui.ApplicationModel.AppInfo.Current.ShowSettingsUI();
    }

    private static Interfaces.PermissionStatus MapPermissionStatus(MauiPermissionStatus status)
    {
        return status switch
        {
            MauiPermissionStatus.Granted => Interfaces.PermissionStatus.Granted,
            MauiPermissionStatus.Denied => Interfaces.PermissionStatus.Denied,
            MauiPermissionStatus.Disabled => Interfaces.PermissionStatus.Disabled,
            MauiPermissionStatus.Restricted => Interfaces.PermissionStatus.Restricted,
            _ => Interfaces.PermissionStatus.Unknown
        };
    }
}
