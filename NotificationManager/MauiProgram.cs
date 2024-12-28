using Microsoft.Extensions.Logging;
using NotificationManager.Database;
using NotificationManager.Repository;
using NotificationManager.Repository.Interfaces;
using NotificationManager.ViewModel;
using NotificationManager.Views;

namespace NotificationManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<DatabaseContext>();
            var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
            builder.Services.AddSingleton<IApplicationRepository, ApplicationRepository>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<NotificationsPage>();
            builder.Services.AddSingleton<NotificationsPageViewModel>();

        return builder.Build();
    }
}
