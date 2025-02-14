using Android.App;
using Android.Content;
using Android.Service.Notification;
using Microsoft.Extensions.Logging;

namespace NotificationManager.Hybrid.Platforms.Android.Services;

[Service(Name = "NotificationManager.Hybrid.Platforms.Android.Services.NotificationBroadcasterService", Enabled = true, Exported = false, Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE")]
[IntentFilter(["android.service.notification.NotificationListenerService"])]
public class NotificationBroadcasterService : NotificationListenerService
{
    private ILogger<NotificationBroadcasterService> _logger;

    public NotificationBroadcasterService()
    {
        _logger = ServiceProvider.GetService<ILogger<NotificationBroadcasterService>>();
    }

    public override void OnCreate()
    {
        base.OnCreate();

        NotificationReceiverService notificationReceiver = new NotificationReceiverService();
        IntentFilter intent = new IntentFilter("com.mycompany.myapp.notificationreceiver");

        RegisterReceiver(notificationReceiver, intent);
    }

    public override void OnNotificationPosted(StatusBarNotification? sbn)
    {
        base.OnNotificationPosted(sbn);
        try
        {
            if (sbn == null) return;

            bool isDismissable = sbn.IsClearable;

            if (!isDismissable) return;

            int notificationId = sbn.Id;
            string? title = sbn.Notification?.Extras?.GetString(Notification.ExtraTitle);
            string? text = sbn.Notification?.Extras?.GetString(Notification.ExtraText);
            long timestamp = sbn.PostTime;
            string? appName = sbn.PackageName;

            // If package name is null, then return
            if (appName == null) return;

            _logger.LogInformation($"Notification received from {appName}");

            Intent intent = new Intent("com.mycompany.myapp.notificationreceiver");

            intent.PutExtra("notificationId", notificationId);
            intent.PutExtra("title", title);
            intent.PutExtra("text", text);
            intent.PutExtra("timestamp", timestamp);
            intent.PutExtra("appName", appName);
            intent.PutExtra("isDismissable", isDismissable);

            SendBroadcast(intent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in OnNotificationPosted");
        }
    }
}
