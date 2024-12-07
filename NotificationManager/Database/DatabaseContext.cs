using SQLite;
using NotificationManager.Entities.Models;

namespace NotificationManager.Database;

public class DatabaseContext
{
    public SQLiteAsyncConnection Database;

    public DatabaseContext()
    {
    }

    public async Task InitializeDatabase()
    {
        if (Database is not null) return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<NotificationDBO>();
    }
}
