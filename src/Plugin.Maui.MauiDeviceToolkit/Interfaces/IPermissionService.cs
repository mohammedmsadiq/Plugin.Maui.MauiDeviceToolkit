namespace Plugin.Maui.MauiDeviceToolkit.Interfaces;

/// <summary>
/// Service for handling runtime permissions across all platforms
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Checks if a specific permission is granted
    /// </summary>
    /// <typeparam name="TPermission">The permission type from Microsoft.Maui.ApplicationModel</typeparam>
    /// <returns>Permission status</returns>
    Task<PermissionStatus> CheckPermissionAsync<TPermission>() where TPermission : Permissions.BasePermission, new();

    /// <summary>
    /// Requests a specific permission from the user
    /// </summary>
    /// <typeparam name="TPermission">The permission type from Microsoft.Maui.ApplicationModel</typeparam>
    /// <returns>Permission status after user interaction</returns>
    Task<PermissionStatus> RequestPermissionAsync<TPermission>() where TPermission : Permissions.BasePermission, new();

    /// <summary>
    /// Opens app settings where user can manually grant permissions
    /// </summary>
    void OpenAppSettings();
}

/// <summary>
/// Permission status enumeration
/// </summary>
public enum PermissionStatus
{
    /// <summary>
    /// Permission has been granted
    /// </summary>
    Granted,

    /// <summary>
    /// Permission has been denied
    /// </summary>
    Denied,

    /// <summary>
    /// Permission is disabled (not available on this device)
    /// </summary>
    Disabled,

    /// <summary>
    /// Permission is restricted (parental controls, etc.)
    /// </summary>
    Restricted,

    /// <summary>
    /// Permission status is unknown
    /// </summary>
    Unknown
}
