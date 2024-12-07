using NotificationManager.Entities.Models;

namespace NotificationManager.Repository.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationDBO>> GetNotificationsAsync(string? appName);
    Task<NotificationDBO> GetNotificationAsync(Guid id);
    Task<int> SaveNotificationAsync(NotificationDBO item);
    Task<int> DeleteNotificationAsync(Guid id);
    Task<List<string?>> GetUniqueAppNamesAsync();
}
