using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Platforms.Android.Helpers;
using NotificationManager.Hybrid.Repository.Interfaces;

namespace NotificationManager.Hybrid.Platforms.Android.Services;

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
        ApplicationDBO newApplication = new ApplicationDBO();
        NotificationDBO notification = new NotificationDBO();

        string? title = intent.GetStringExtra("title");
        string? text = intent.GetStringExtra("text");
        long? timestamp = intent.GetLongExtra("timestamp", 0);
        string? packageName = intent.GetStringExtra("appName");

        if (title == null && text == null) return;
        if (packageName == null) return;

        ApplicationDBO? application = await _applicationRepository.GetApplicationAsync(packageName);
        if (application == null)
        {
            PackageManager? packageManager = context?.PackageManager;
            if (packageManager == null) return;

            ApplicationInfo applicationInfo;
            try
            {
                applicationInfo = packageManager.GetApplicationInfo(packageName, 0);
            }
            catch (PackageManager.NameNotFoundException)
            {
                //TODO: Handle missed notification due to null ApplicationInfo
                return;
            }

            string? appName = null;
            Drawable? appLogo = null;
            if (applicationInfo != null)
            {
                appName = packageManager.GetApplicationLabel(applicationInfo);
                appLogo = packageManager.GetApplicationLogo(applicationInfo);
                if (appLogo == null)
                {
                    appLogo = applicationInfo.LoadIcon(packageManager);
                }
            }

            newApplication = new ApplicationDBO()
            {
                Name = appName,
                Package = packageName,
                Icon = appLogo != null ? ImageHelper.DrawableToByteArray(appLogo) : null
            };

            await _applicationRepository.SaveApplicationAsync(newApplication);

            notification = new NotificationDBO()
            {
                NotificationTitle = title,
                NotificationText = text,
                NotificationApp = packageName,
                ApplicationId = newApplication.Id,
                RecievedOn = DateTimeHelper.FromTimestamp(timestamp.Value)
            };

            await _notificationRepository.SaveNotificationAsync(notification);
        }
        else
        {
            notification = new NotificationDBO
            {
                NotificationTitle = title,
                NotificationText = text,
                NotificationApp = packageName,
                ApplicationId = application.Id,
                RecievedOn = DateTimeHelper.FromTimestamp(timestamp.Value)
            };

            await _notificationRepository.SaveNotificationAsync(notification);
        }
    }
}
