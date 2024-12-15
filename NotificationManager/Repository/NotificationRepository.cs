﻿using NotificationManager.Database;
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

    public async Task<List<NotificationDBO>> GetNotificationsAsync(string? appName)
    {
        IQueryable<NotificationDBO> query = _databaseContext.Notification.Take(10);
        if (appName != null)
        {
            query = query.Where(notif => notif.NotificationApp == appName);
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

    public async Task<List<NotificationCountDTO>> GetUniqueAppNamesAsync()
    {
        List<NotificationDBO> appList = await _databaseContext.Notification.ToListAsync();

        List<string?> appNameLists = appList.Select(app => app.NotificationApp).Distinct().ToList();

        List<NotificationCountDTO> notificationCountList = new List<NotificationCountDTO>();
        foreach (string? appName in appNameLists) 
        {
            if (appName == null) continue;
            NotificationCountDTO notifCount = new NotificationCountDTO()
            {
                AppName = appName,
                Count = await GetAppNotificationCount(appName)
            };
            notificationCountList.Add(notifCount);
        }

        return notificationCountList;
    }

    public async Task<int> GetAppNotificationCount(string appName)
    {
        List<NotificationDBO> appList = await _databaseContext.Notification.Where(app => app.NotificationApp == appName).ToListAsync();

        return appList.Count();
    }
}
