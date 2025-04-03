using Ebisx.POS.Presentation.Common;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class NavigationService : INavigationService
{
    private readonly ISettingsService _settingService;
    public NavigationService(ISettingsService settingsService)
    {
        _settingService = settingsService;
    }
        
    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        return
            routeParameters != null
                ? Shell.Current.GoToAsync(route, routeParameters)
                : Shell.Current.GoToAsync(route);
    }

    public Task InitializeAsync()
    {
        return NavigateToAsync(_settingService.IsUserLoggedIn
            ? AppRoutes.Home
            : AppRoutes.Login);
    }

    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task Logout()
    {
        await NavigateToAsync(AppRoutes.Login);
    }
}
