using Microsoft.EntityFrameworkCore;
using NotificationManager.Entities.Models;

namespace NotificationManager.Database;

public class DatabaseContext : DbContext
{
    public DbSet<NotificationDBO> Notification { get; set; }



    public string DbPath { get; set; }

    public DatabaseContext()
    {
        DbPath = Constants.DatabasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
