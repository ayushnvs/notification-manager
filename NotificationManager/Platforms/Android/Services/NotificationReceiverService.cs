using Android.App;
using Android.Content;

namespace NotificationManager.Platforms.Android.Services;

[BroadcastReceiver(Enabled = true, Permission = "android.Manifest.Permission.RECEIVE_BOOT_COMPLETED")]
[IntentFilter(["intentNotificationRecieved"])]
public class NotificationReceiverService : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        if (intent == null) return;

        string? title = intent.GetStringExtra("title");
        string? text = intent.GetStringExtra("text");
        long? timestamp = intent.GetLongExtra("timestamp", 0);
        string? appName = intent.GetStringExtra("appName");


    }
}
