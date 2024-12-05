using Android.App;
using Android.Content;
using Android.Service.Notification;

namespace NotificationManager.Platforms.Android.Services;

[Service(Name = "NotificationManager.Platforms.Android.Services.NotificationBroadcasterService", Enabled = true, Exported = false, Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE")]
[IntentFilter(["android.service.notification.NotificationListenerService"])]
public class NotificationBroadcasterService : NotificationListenerService
{
    public override void OnNotificationPosted(StatusBarNotification? sbn)
    {
        base.OnNotificationPosted(sbn);
        if (sbn == null) return;

        string? title = sbn.Notification?.Extras?.GetString(Notification.ExtraTitle);
        string? text = sbn.Notification?.Extras?.GetString(Notification.ExtraText);
        long timestamp = sbn.PostTime;
        string? appName = sbn.PackageName;

        if (appName == null) return;

        Intent intent = new Intent("intNotificationRecieved");
        intent.PutExtra("title", title);
        intent.PutExtra("text", text);
        intent.PutExtra("timestamp", timestamp);
        intent.PutExtra("appName", appName);

        SendBroadcast(intent);
    }
}
