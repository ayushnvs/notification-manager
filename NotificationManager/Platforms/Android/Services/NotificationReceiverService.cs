using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using NotificationManager.Entities.Models;
using NotificationManager.Platforms.Android.Helpers;
using NotificationManager.Repository.Interfaces;

namespace NotificationManager.Platforms.Android.Services;

[BroadcastReceiver(Enabled = true, Permission = "android.Manifest.Permission.RECEIVE_BOOT_COMPLETED")]
[IntentFilter(["intentNotificationRecieved"])]
public class NotificationReceiverService : BroadcastReceiver
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IApplicationRepository _applicationRepository;

    public NotificationReceiverService()
    {
        _notificationRepository = ServiceProvider.GetService<INotificationRepository>();
        _applicationRepository = ServiceProvider.GetService<IApplicationRepository>();
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
            //TODO: Handled missed notification due to null ApplicationInfo
            return;
        }

        string? appName = null;
        Drawable? appLogo = null;
        if (applicationInfo != null)
        {
            appName = packageManager.GetApplicationLabel(applicationInfo);
            appLogo = packageManager.GetApplicationLogo(applicationInfo);
        }

        ApplicationDBO? application = await _applicationRepository.GetApplicationAsync(packageName);
        if (application == null) 
        {
            application = new ApplicationDBO()
            {
                Name = appName,
                Package = packageName,
                Icon = appLogo != null ? ImageHelper.DrawableToByteArray(appLogo) : null
            };

            await _applicationRepository.SaveApplicationAsync(application);
        }

        NotificationDBO notification = new NotificationDBO
        {
            NotificationTitle = title,
            NotificationText = text,
            NotificationApp = packageName,
            ApplicationId = application.Id,
            RecievedOn = new DateTime(timestamp.Value)
        };

        await _notificationRepository.SaveNotificationAsync(notification);
    }
}
