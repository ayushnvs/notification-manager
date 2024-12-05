using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Provider;

namespace NotificationManager;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (!IsNotificationListenerServiceEnabled()) RequestNotificationListenerPermission();
    }

    private void RequestNotificationListenerPermission()
    {

    }

    private bool IsNotificationListenerServiceEnabled()
    {
        string? packageName = PackageName;
        string? flat = Settings.Secure.GetString(ContentResolver, "enabled_notification_listeners");

        return false;
    }
}
