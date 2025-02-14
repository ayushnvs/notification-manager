using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Text;
using Android.Widget;
using NotificationManager.Hybrid.Platforms.Android.Services;

namespace NotificationManager.Hybrid;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (!IsNotificationListenerServiceEnabled()) RequestNotificationListenerPermission();
        else
        {
            bool isServiceRunning = IsServiceRunning(this, typeof(NotificationBroadcasterService));
            if (!isServiceRunning) StartService(new Intent(this, typeof(NotificationBroadcasterService)));
        }

        //PermissionStatus permissionStatus = await CheckAndRequestStoragePermission();
    }

    private void RequestNotificationListenerPermission()
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.SetTitle("Permission Required");
        builder.SetMessage("Please enable notification listener access for this app.");
        builder.SetPositiveButton("Go to Settings", (sender, args) =>
        {
            Intent intent = new Intent(Settings.ActionNotificationListenerSettings);
            StartActivity(intent);
        });
        builder.SetNegativeButton("Cancel", (sender, args) =>
        {
            Toast.MakeText(this, "Permission denied. The app will not function properly.", ToastLength.Short)?.Show();
        });
        builder.Show();
    }

    private bool IsNotificationListenerServiceEnabled()
    {
        string? packageName = PackageName;
        string? flat = Settings.Secure.GetString(ContentResolver, "enabled_notification_listeners");

        if (string.IsNullOrEmpty(flat)) return false;

        string[] names = flat.Split(':');
        foreach (string name in names)
        {
            ComponentName? componentName = ComponentName.UnflattenFromString(name);
            if (componentName != null && TextUtils.Equals(packageName, componentName.PackageName)) return true;
        }

        return false;
    }

    public bool IsServiceRunning(Context context, Type serviceClass)
    {
        ActivityManager activityManager = (ActivityManager)context.GetSystemService(ActivityService);
        IList<ActivityManager.RunningServiceInfo> runningServices = activityManager.GetRunningServices(100);

        foreach (ActivityManager.RunningServiceInfo service in runningServices)
        {
            if (service.Service?.ClassName == serviceClass.FullName) return true;
        }

        return false;
    }

    public async Task<PermissionStatus> CheckAndRequestStoragePermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        }
        return status;
    }
}
