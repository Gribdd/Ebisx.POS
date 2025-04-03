
namespace Ebisx.POS.Presentation.ViewModels.Manager;

[QueryProperty(nameof(User), nameof(User))]
public partial class ManagerHomePageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    [ObservableProperty]
    public partial User User { get; set; } = new();

    public ManagerHomePageViewModel(
        INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task Logout()   
    {
        await _navigationService.Logout();
    }

    [RelayCommand]
    private async Task NavigateToInventory()
    {
        await _navigationService.NavigateToAsync(AppRoutes.ManagerInventory);
    }

    [RelayCommand]
    private async Task NavigateToTransactions()
    {
        await _navigationService.NavigateToAsync(AppRoutes.ManagerTransaction);
    }

    [RelayCommand]
    private async Task NavigateToSales()
    {
        await _navigationService.NavigateToAsync(AppRoutes.ManagerSales);
    }

    [RelayCommand]
    private async Task NavigateToEmployees()
    {
        await _navigationService.NavigateToAsync(AppRoutes.ManagerEmployees);
    }
}
