namespace NotificationManager;

public static class ServiceProvider
{
    public static IServiceProvider? Current => IPlatformApplication.Current != null ? IPlatformApplication.Current.Services : null;
    public static TService GetService<TService>()
    {
        return Current.GetService<TService>();
    }
}