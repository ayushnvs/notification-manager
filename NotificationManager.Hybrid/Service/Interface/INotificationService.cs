using NotificationManager.Hybrid.Entities.Models;

namespace NotificationManager.Hybrid.Service.Interface;

public interface INotificationService
{
    Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName);
    Task DeleteAllNotification(string package);
    Task DeleteAllNotification(List<string> packages);
    Task DeleteNotificationAsync(Guid id);
    Task DeleteNotificationAsync(List<Guid> idList);
}
