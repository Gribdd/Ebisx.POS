namespace Ebisx.POS.Presentation;

public static class Constants
{
    public const string DatabaseFilename = "ebisx_pos.db3";

    //public const SQLite.SQLiteOpenFlags Flags =
    //// open the database in read/write mode
    //    SQLite.SQLiteOpenFlags.ReadWrite |
    //// create the database if it doesn't exist
    //    SQLite.SQLiteOpenFlags.Create |
    //// enable multi-threaded database access
    //    SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static readonly string BaseAddress = GetBaseAddress();

    private static string GetBaseAddress()
    {
        return DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5000"  // Android emulator (maps to localhost)
            : "http://localhost:5000"; // Other platforms (Windows, macOS)
    }
}
