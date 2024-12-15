using Android.App;
using Android.Content;
using Android.Content.PM;
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
        string? packageName = intent.GetStringExtra("appName");

        if (title == null && text == null) return;
        if (packageName == null) return;


        PackageManager? packageManager = context?.PackageManager;
        if (packageManager == null) return;

        ApplicationInfo applicationInfo;
        try
        {
            applicationInfo = packageManager.GetApplicationInfo(packageName, 0);
        }
        catch (PackageManager.NameNotFoundException)
        {
            return;
        }

        string appName = packageManager.GetApplicationLabel(applicationInfo);


        NotificationDBO notification = new NotificationDBO
        {
            NotificationTitle = title,
            NotificationText = text,
            NotificationApp = packageName,
            RecievedOn = new DateTime(timestamp.Value)
        };

        await _notificationRepository.SaveNotificationAsync(notification);
    }
}
