namespace Ebisx.POS.Presentation.Services.Interface;

public interface INavigationService
{
    Task InitializeAsync();
    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);
    Task GoBack();
    Task Logout();
}
