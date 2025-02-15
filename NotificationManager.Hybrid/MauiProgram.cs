using Microsoft.Extensions.Logging;
using NotificationManager.Hybrid.Database;
using NotificationManager.Hybrid.Service;
using NotificationManager.Hybrid.Service.Interface;
using NotificationManager.Hybrid.Repository;
using NotificationManager.Hybrid.Repository.Interfaces;
using Serilog;
using CommunityToolkit.Maui;

namespace NotificationManager.Hybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        // Get download directory path
#if __ANDROID__

        string logFilePath = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsoluteFile.Path.ToString();
        builder.Services.AddSerilog
            (
                new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Debug()
                    .WriteTo.File(Path.Combine(logFilePath, "log.txt"), rollingInterval: RollingInterval.Day)
                    .CreateLogger()
            );
#endif



#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddDbContext<DatabaseContext>();
        DatabaseContext dbContext = new DatabaseContext();
        dbContext.Database.EnsureCreated();
        dbContext.Dispose();

        // Register Repositories
        builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
        builder.Services.AddSingleton<IApplicationRepository, ApplicationRepository>();

        // Register Services
        builder.Services.AddSingleton<INotificationService, NotificationService>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}
