﻿using Bogus.DataSets;
using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Common;
using Ebisx.POS.Presentation.Views.Manager;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    //[ObservableProperty]
    //private ObservableCollection<string> _mockEmails;
    //[ObservableProperty]
    //private User _user = new();
    private ObservableCollection<User> _mockUsers = new();

    [ObservableProperty]
    public partial ObservableCollection<string> MockEmails { get; set; } = new();

    [ObservableProperty]
    public partial User User { get; set; } = new();


    [RelayCommand]
    private async Task Authenticate()
    {
        User user = _mockUsers.FirstOrDefault(u => u.Email == User.Email && u.Password == User.Password)!;

        if (user is not null)
        {
            if (user.UserRole == UserRole.Employee)
            {
                await Shell.Current.GoToAsync("///home");
                return;
            }

            if (user.UserRole == UserRole.Manager)
            {
                await Shell.Current.GoToAsync("///managerhome");
                return;
            }
        }

        // Show error message
        await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
    }

    public void LoadMockEmails()
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => "123")
            .RuleFor(u => u.BirthDate, f => f.Date.Past(30));

        _mockUsers = new ObservableCollection<User>(userFaker.Generate(2));
        _mockUsers[0].UserRole = UserRole.Employee;
        _mockUsers[1].UserRole = UserRole.Manager;

        //map the emails to the mockEmails collection
        MockEmails = new ObservableCollection<string>(_mockUsers.Select(u => u.Email));
    }

}
