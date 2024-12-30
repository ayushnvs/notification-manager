using NotificationManager.Entities.DTO;
using NotificationManager.Entities.Models;

namespace NotificationManager.Repository.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName);
    Task<NotificationDBO?> GetNotificationAsync(Guid id);
    Task<int> SaveNotificationAsync(NotificationDBO item);
    Task<int> GetAppNotificationCount(Guid appId);
    Task<int> DeleteNotificationAsync(Guid id);
    Task<int> DeleteNotificationAsync(string package);
}
