using Android.App;
using Android.Content;
using Android.Service.Notification;

namespace NotificationManager.Hybrid.Platforms.Android.Services;

[Service(Name = "NotificationManager.Hybrid.Platforms.Android.Services.NotificationBroadcasterService", Enabled = true, Exported = false, Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE")]
[IntentFilter(["android.service.notification.NotificationListenerService"])]
public class NotificationBroadcasterService : NotificationListenerService
{
    public override void OnNotificationPosted(StatusBarNotification? sbn)
    {
        base.OnNotificationPosted(sbn);
        if (sbn == null) return;

        bool isDismissable = sbn.IsClearable;

        int notificationId = sbn.Id;
        string? title = sbn.Notification?.Extras?.GetString(Notification.ExtraTitle);
        string? text = sbn.Notification?.Extras?.GetString(Notification.ExtraText);
        long timestamp = sbn.PostTime;
        string? appName = sbn.PackageName;

        // If package name is null, then return
        if (appName == null) return;

        Intent intent = new Intent("com.mycompany.myapp.notificationreceiver");

        intent.PutExtra("notificationId", notificationId);
        intent.PutExtra("title", title);
        intent.PutExtra("text", text);
        intent.PutExtra("timestamp", timestamp);
        intent.PutExtra("appName", appName);
        intent.PutExtra("isDismissable", isDismissable);

        SendBroadcast(intent);
    }
}
