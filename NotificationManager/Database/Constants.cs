namespace NotificationManager.Database;

public static class Constants
{
    public const string DatabaseFilename = "NotisSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}



// TODO: Check following documents and repo to configure database
/* Document: https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0
 * Rep: https://github.com/dotnet/maui-samples/tree/main/9.0/Data/TodoSQLite
 */
