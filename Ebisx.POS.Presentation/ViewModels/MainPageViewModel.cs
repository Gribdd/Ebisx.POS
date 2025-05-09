﻿using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Common;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly ISettingsService _settingService;
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    [ObservableProperty]
    public partial User User { get; set; } = new();
    [ObservableProperty]
    public partial ObservableCollection<User> Users { get; set; } = new(); 
    [ObservableProperty]
    public partial ObservableCollection<string> UsersEmailAddress { get; set; } = new();

    public MainPageViewModel(
        ISettingsService settingsService,
        INavigationService navigationService,
        IUserService userService)
    {
        _settingService = settingsService;
        _navigationService = navigationService;
        _userService = userService;
    }

    [RelayCommand]
    private async Task Authenticate()
    {
        User user = Users.FirstOrDefault(
            u => u.EmailAddress == User.EmailAddress && 
            u.Password == User.Password)!;

        if (user is not null)
        {
            if (user.UserRole.Role == "employee")
            {
                await _navigationService.NavigateToAsync(AppRoutes.Home );
            }

            if (user.UserRole.Role == "manager")
            {
                await _navigationService.NavigateToAsync(
                    AppRoutes.ManagerHome,
                    new Dictionary<string, object> {
                        { "User", user }
                    });
            }
            _settingService.IsUserLoggedIn = true;
            _settingService.AuthUserId = user.PrivateId;
        }
        else
        {
            // Show error message
            await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
        }

    }



    public async Task LoadAccounts()
    {
        Users = new ObservableCollection<User>(await _userService.GetUsersAsync());
        UsersEmailAddress = new ObservableCollection<string>(
            Users.Select(u => u.EmailAddress));
    }
}
