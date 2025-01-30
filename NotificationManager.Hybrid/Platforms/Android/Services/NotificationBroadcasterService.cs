﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Service.Notification;
using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Platforms.Android.Helpers;
using NotificationManager.Hybrid.Repository.Interfaces;

namespace NotificationManager.Hybrid.Platforms.Android.Services;

[Service(Name = "NotificationManager.Platforms.Android.Services.NotificationBroadcasterService", Enabled = true, Exported = false, Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE")]
[IntentFilter(["android.service.notification.NotificationListenerService"])]
public class NotificationBroadcasterService : NotificationListenerService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IApplicationRepository _applicationRepository;

    public NotificationBroadcasterService()
    {
        _notificationRepository = ServiceProvider.GetService<INotificationRepository>();
        _applicationRepository = ServiceProvider.GetService<IApplicationRepository>();
    }

    public override void OnNotificationPosted(StatusBarNotification? sbn)
    {
        base.OnNotificationPosted(sbn);
        if (sbn == null) return;

        int notificationId = sbn.Id;
        string? title = sbn.Notification?.Extras?.GetString(Notification.ExtraTitle);
        string? text = sbn.Notification?.Extras?.GetString(Notification.ExtraText);
        long timestamp = sbn.PostTime;
        string? appName = sbn.PackageName;

        if (appName == null) return;

        Intent intent = new Intent("intentNotificationRecieved");

        intent.PutExtra("notificationId", notificationId);
        intent.PutExtra("title", title);
        intent.PutExtra("text", text);
        intent.PutExtra("timestamp", timestamp);
        intent.PutExtra("appName", appName);

        SendBroadcast(intent);
    }
}
