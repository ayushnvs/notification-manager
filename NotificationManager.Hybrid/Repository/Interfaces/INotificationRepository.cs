using NotificationManager.Hybrid.Entities.Models;

namespace NotificationManager.Hybrid.Repository.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName);
    Task<NotificationDBO?> GetNotificationAsync(Guid id);
    Task<NotificationDBO?> GetNotificationAsync(int notificationId);
    Task<int> SaveNotificationAsync(NotificationDBO item);
    Task<int> GetAppNotificationCount(Guid appId);
    Task<int> DeleteNotificationAsync(Guid id);
    Task<int> DeleteNotificationAsync(string package);
}
