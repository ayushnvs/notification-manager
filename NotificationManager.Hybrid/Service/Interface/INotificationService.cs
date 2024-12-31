namespace NotificationManager.Hybrid.Service.Interface;

public interface INotificationService
{
    Task DeleteAllNotification(string package);
    Task DeleteAllNotification(List<string> packages);
}
