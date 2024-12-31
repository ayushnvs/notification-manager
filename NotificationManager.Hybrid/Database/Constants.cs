namespace NotificationManager.Hybrid.Database;

public static class Constants
{
    public const string DatabaseFilename = "NotisSQLite.db3";

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}



// TODO: Check following documents and repo to configure database
/* Document: https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0
 * Rep: https://github.com/dotnet/maui-samples/tree/main/9.0/Data/TodoSQLite
 */
