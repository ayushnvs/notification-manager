using SQLite;
using NotificationManager.Database;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;

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
        await _databaseContext.InitializeDatabase();
        AsyncTableQuery<NotificationDBO> query = _databaseContext.Database.Table<NotificationDBO>();
        if (appName != null)
        {
            query = query.Where(notif => notif.NotificationApp == appName);
        }
        return await query.ToListAsync();
    }

    public async Task<NotificationDBO> GetNotificationAsync(Guid id)
    {
        await _databaseContext.InitializeDatabase();
        return await _databaseContext.Database.Table< NotificationDBO>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveNotificationAsync(NotificationDBO item)
    {
        await _databaseContext.InitializeDatabase();

        NotificationDBO? notif = await GetNotificationAsync(item.Id);

        if (notif != null)
        {
            return await _databaseContext.Database.UpdateAsync(item);
        }
        else
        {
            return await _databaseContext.Database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteNotificationAsync(Guid id)
    {
        await _databaseContext.InitializeDatabase();
        NotificationDBO item = await GetNotificationAsync(id);
        return await _databaseContext.Database.DeleteAsync(item);
    }

    public async Task<List<string?>> GetUniqueAppNamesAsync()
    {
        await _databaseContext.InitializeDatabase();

        List<NotificationDBO> appList = await _databaseContext.Database
            .Table<NotificationDBO>().ToListAsync();

        return appList.Select(app => app.NotificationApp).Distinct().ToList();
    }
}
