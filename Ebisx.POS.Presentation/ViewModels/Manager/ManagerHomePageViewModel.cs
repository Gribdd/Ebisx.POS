using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Manager
{
    public partial class ManagerHomePageViewModel : BaseViewModel
    {
        [RelayCommand]
        private void Logout()
        {
            Shell.Current.GoToAsync("//mainpage");
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
