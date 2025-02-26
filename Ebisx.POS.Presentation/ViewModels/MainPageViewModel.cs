using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Models;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> _mockEmails;

    private ObservableCollection<User> _mockUsers;

    [ObservableProperty]
    private User _user = new();


    [RelayCommand]
    private async Task Authenticate()
    {
        User user = _mockUsers.FirstOrDefault(u => u.Email == User.Email && u.Password == User.Password);
        if (user != null)
        {
            await Shell.Current.GoToAsync("//home");
            return;
        }

        // Show error message
        await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password.", "OK");

    }

    private void LoadEmails()
    {

    }

    public void LoadMockEmails()
    {
        var userFaker = new Faker<User>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Password, f => "123")
        .RuleFor(u => u.BirthDate, f => f.Date.Past(30));

        var fakePerson = userFaker.Generate(10);
        _mockUsers = new ObservableCollection<User>(fakePerson);

        //map the emails to the mockEmails collection
        MockEmails = new ObservableCollection<string>(_mockUsers.Select(u => u.Email));
    }

}
