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

    public async Task<List<NotificationDBO>> GetNotificationsAsync()
    {
        await _databaseContext.InitializeDatabase();
        return await _databaseContext.Database.Table<NotificationDBO>().ToListAsync();
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
}
