﻿using Microsoft.EntityFrameworkCore;
using NotificationManager.Hybrid.Entities.Models;

namespace NotificationManager.Hybrid.Database;

public class DatabaseContext : DbContext
{
    public DbSet<NotificationDBO> Notification { get; set; }
    public DbSet<ApplicationDBO> Application { get; set; }


    public string DbPath { get; set; }

    public DatabaseContext()
    {
        DbPath = Constants.DatabasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
