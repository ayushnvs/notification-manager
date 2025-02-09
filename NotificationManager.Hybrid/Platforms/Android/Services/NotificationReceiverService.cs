using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Platforms.Android.Helpers;
using NotificationManager.Hybrid.Repository.Interfaces;
using NotificationManager.Hybrid.Service.Interface;

namespace NotificationManager.Hybrid.Platforms.Android.Services;

[BroadcastReceiver(Enabled = true, Exported = true, Permission = "android.Manifest.Permission.RECEIVE_BOOT_COMPLETED")]
[IntentFilter(["com.mycompany.myapp.notificationreceiver"])]
public class NotificationReceiverService : BroadcastReceiver
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly INotificationService _notificationService;

    public NotificationReceiverService()
    {
        _notificationRepository = ServiceProvider.GetService<INotificationRepository>();
        _applicationRepository = ServiceProvider.GetService<IApplicationRepository>();
        _notificationService = ServiceProvider.GetService<INotificationService>();
    }

    public async override void OnReceive(Context? context, Intent? intent)
    {
        if (intent == null) return;

        // Retrieve notification data from the intent
        int notificationId = intent.GetIntExtra("notificationId", 0);
        string? title = intent.GetStringExtra("title");
        string? text = intent.GetStringExtra("text");
        long? timestamp = intent.GetLongExtra("timestamp", 0);
        string? packageName = intent.GetStringExtra("appName");
        bool? isDismissable = intent.GetBooleanExtra("isDismissable", true);

        if (title == null && text == null) return;
        if (packageName == null) return;

        Guid? applicationId = null;
        ApplicationDBO? application = await _applicationRepository.GetApplicationAsync(packageName);

        if (application != null) applicationId = application.Id;
        else applicationId = await CreateApplicationRecord(context, packageName);

        if (isDismissable == false)
        {
            NotificationDBO? duplicateNotification = await _notificationRepository.GetNotificationAsync(packageName, notificationId);
            if (duplicateNotification != null) return;
        }

        NotificationDBO notification = new NotificationDBO()
        {
            NotificationId = notificationId,
            NotificationTitle = title,
            NotificationText = text,
            NotificationApp = packageName,
            ApplicationId = applicationId.Value,
            RecievedOn = DateTimeHelper.FromTimestamp(timestamp.Value)
        };

        if (application != null)
        {
            // Check if it is duplicate notification
            bool isDuplicate = await _notificationService.CheckDuplicateNotificationAsync(notification);
            if (isDuplicate) return;
        }

        await _notificationRepository.SaveNotificationAsync(notification);
    }
    
    private async Task<Guid?> CreateApplicationRecord(Context? context, string? packageName)
    {
        PackageManager? packageManager = context?.PackageManager;
        if (packageManager == null) return null;

        ApplicationInfo? applicationInfo = null;
        try
        {
            applicationInfo = packageManager.GetApplicationInfo(packageName, 0);
        }
        catch (PackageManager.NameNotFoundException)
        {
            //TODO: Handle missed notification due to null ApplicationInfo
            return null;
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

            ApplicationDBO newApplication = new ApplicationDBO()
            {
                Name = appName,
                Package = packageName,
                Icon = appLogo != null ? ImageHelper.DrawableToByteArray(appLogo) : null
            };

            await _applicationRepository.SaveApplicationAsync(newApplication);

            return newApplication.Id;
        }
        return null;
    }
}
