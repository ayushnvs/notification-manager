using Android.App;
using Android.Content;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;

namespace NotificationManager.Platforms.Android.Services;

[BroadcastReceiver(Enabled = true, Permission = "android.Manifest.Permission.RECEIVE_BOOT_COMPLETED")]
[IntentFilter(["intentNotificationRecieved"])]
public class NotificationReceiverService : BroadcastReceiver
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationReceiverService()
    {
        _notificationRepository = ServiceProvider.GetService<INotificationRepository>();
    }

    public async override void OnReceive(Context? context, Intent? intent)
    {
        if (intent == null) return;

        string? title = intent.GetStringExtra("title");
        string? text = intent.GetStringExtra("text");
        long? timestamp = intent.GetLongExtra("timestamp", 0);
        string? appName = intent.GetStringExtra("appName");

        NotificationDBO notification = new NotificationDBO
        {
            NotificationTitle = title,
            NotificationText = text,
            NotificationApp = appName,
            RecievedOn = new DateTime(timestamp.Value)
        };

        await _notificationRepository.SaveNotificationAsync(notification);
    }
}
