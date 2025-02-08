using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Service.Interface;
using NotificationManager.Hybrid.Repository.Interfaces;

namespace NotificationManager.Hybrid.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName)
    {
        return await _notificationRepository.GetNotificationsAsync(packageName);
    }

    public async Task<bool> CheckDuplicateNotificationAsync(NotificationDBO notification)
    {
        int debouncingPeriod = 1500;
        bool isDuplicate = false;

        List<NotificationDBO> notifsList = await _notificationRepository.GetNotificationsAsync(notification.NotificationApp, notification.RecievedOn, notification.RecievedOn.Subtract(TimeSpan.FromMilliseconds(debouncingPeriod)));

        if (notifsList.Count > 0)
        {
            foreach (NotificationDBO notif in notifsList)
            {
                if (notif.NotificationTitle == notification.NotificationTitle && notif.NotificationText == notification.NotificationText)
                {
                    isDuplicate = true;
                    break;
                }
            }
        }

        if (isDuplicate) return true;
        else return false;
    }

    public async Task DeleteAllNotification(string package)
    {
        await _notificationRepository.DeleteNotificationAsync(package);
    }

    public async Task DeleteAllNotification(List<string> packages)
    {
        foreach (var package in packages) 
        { 
            await DeleteAllNotification(package);
        }
    }

    public async Task DeleteNotificationAsync(Guid id)
    {
        await _notificationRepository.DeleteNotificationAsync(id);
    }

    public async Task DeleteNotificationAsync(List<Guid> idList)
    {
        foreach(Guid id in idList)
        {
            await DeleteNotificationAsync(id);
        }
    }
}
