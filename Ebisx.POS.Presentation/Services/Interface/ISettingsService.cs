namespace Ebisx.POS.Presentation.Services.Interface;

public interface ISettingsService
{
    int AuthUserId { get; set; }
    bool IsUserLoggedIn { get; set; }
    void Clear();
    void    Logout();
}
