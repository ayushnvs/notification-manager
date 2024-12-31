using NotificationManager.Hybrid.Service.Interface;
using NotificationManager.Repository.Interfaces;

namespace NotificationManager.Hybrid.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
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
}
