using NotificationManager.Hybrid.Database;
using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NotificationManager.Hybrid.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly DatabaseContext _databaseContext;

    public NotificationRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName)
    {
        IQueryable<NotificationDBO> query = _databaseContext.Notification.Include(notif => notif.Application);
        if (packageName != null)
        {
            query = query.Where(notif => notif.NotificationApp == packageName);
        }
        return await query.ToListAsync();
    }

    public async Task<NotificationDBO?> GetNotificationAsync(Guid id)
    {
        return await _databaseContext.Notification.Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveNotificationAsync(NotificationDBO item)
    {
        await _databaseContext.Notification.AddAsync(item);
        return await _databaseContext.SaveChangesAsync();
    }

    public async Task<int> DeleteNotificationAsync(Guid id)
    {
        NotificationDBO? item = await GetNotificationAsync(id);
        if (item == null) return 1;
        _databaseContext.Notification.Remove(item);
        return await _databaseContext.SaveChangesAsync();
    }

    public async Task<int> DeleteNotificationAsync(string package)
    {
        IQueryable<NotificationDBO> query = _databaseContext.Notification.Where(notif => notif.NotificationApp == package);

        _databaseContext.Notification.RemoveRange(query);
        return await _databaseContext.SaveChangesAsync();
    }

    public async Task<int> GetAppNotificationCount(Guid appId)
    {
        List<NotificationDBO> appList = await _databaseContext.Notification.Where(app => app.ApplicationId == appId).ToListAsync();

        return appList.Count();
    }
}
