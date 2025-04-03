namespace Ebisx.POS.Presentation.Services;

public sealed class SettingsService : ISettingsService
{
    private const string UserIdKey = "user_id";
    private const int UserIdKeyDefault = -1; // -1 means no user is logged in

    private const string UserLoggedInKey = "user_logged_in";
    private const bool UserLoggedInKeyDefault = false;
    public int AuthUserId
    {
        get => Preferences.Get(UserIdKey, UserIdKeyDefault); 
        set => Preferences.Set(UserIdKey, value);
    }

    public bool IsUserLoggedIn
    {
        get => Preferences.Get(UserLoggedInKey, UserLoggedInKeyDefault);
        set => Preferences.Set(UserLoggedInKey, value);
    }

    public void Clear()
    {
        Preferences.Clear();
    }

    public void Logout()
    {
        Preferences.Remove(UserIdKey);
        Preferences.Remove(UserLoggedInKey);
    }
}