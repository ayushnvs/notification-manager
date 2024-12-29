using NotificationManager.Database;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;
using NotificationManager.Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace NotificationManager.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly DatabaseContext _databaseContext;

    public NotificationRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<NotificationDBO>> GetNotificationsAsync(string? packageName)
    {
        IQueryable<NotificationDBO> query = _databaseContext.Notification;
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

    public async Task<List<ApplicationViewDTO>> GetUniqueAppNamesAsync()
    {
        List<NotificationDBO> appList = await _databaseContext.Notification.ToListAsync();

        List<ApplicationDBO?> appLists = appList.Select(app => app.Application).Distinct().ToList();

        List<ApplicationViewDTO> notificationCountList = new List<ApplicationViewDTO>();
        foreach (ApplicationDBO? app in appLists) 
        {
            if (app == null) continue;

            ApplicationViewDTO notifCount = new ApplicationViewDTO()
            {
                PackageName = app.Package,
                AppName = app.Name,
                AppLogo = app.Icon != null ? $"data:jpg;base64,${Convert.ToBase64String(app.Icon)}" : null,
                //ShowDefaultAppIcon = app.Icon == null ? true : false,
                Count = await GetAppNotificationCount(app.Id)
            };
            notificationCountList.Add(notifCount);
        }

        return notificationCountList;
    }

    public async Task<int> GetAppNotificationCount(Guid appId)
    {
        List<NotificationDBO> appList = await _databaseContext.Notification.Where(app => app.ApplicationId == appId).ToListAsync();

        return appList.Count();
    }
}
