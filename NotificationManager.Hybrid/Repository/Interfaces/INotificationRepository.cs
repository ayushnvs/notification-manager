using NotificationManager.Hybrid.Entities.Models;

namespace NotificationManager.Hybrid.Repository.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName);
    Task<NotificationDBO?> GetNotificationAsync(Guid id);
    Task<NotificationDBO?> GetNotificationAsync(string packageName, int notificationId);
    Task<List<NotificationDBO>> GetNotificationsAsync(string packageName, DateTime date1, DateTime date2);
    Task<int> SaveNotificationAsync(NotificationDBO item);
    Task<int> GetAppNotificationCount(Guid appId);
    Task<int> DeleteNotificationAsync(Guid id);
    Task<int> DeleteNotificationAsync(string package);
}
