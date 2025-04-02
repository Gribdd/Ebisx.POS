using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.ViewModels.Manager
{
    public partial class ManagerHomePageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ManagerHomePageViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void Logout()   
        {
            _navigationService.Logout();
        }

        [RelayCommand]
        private void NavigateToInventory()
        {
            Shell.Current.GoToAsync("inventory");
        }

        [RelayCommand]
        private void NavigateToTransactions()
        {
            Shell.Current.GoToAsync("transaction");
        }

        [RelayCommand]
        private void NavigateToSales()
        {
            Shell.Current.GoToAsync("sales");
        }

        [RelayCommand]
        private void NavigateToEmployees()
        {
            Shell.Current.GoToAsync("employee");
        }
    }
}
