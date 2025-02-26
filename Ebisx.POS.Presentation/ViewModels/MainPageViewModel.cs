using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> _mockEmails;

    private ObservableCollection<User> _mockUsers;

    [ObservableProperty]
    private User _user = new();


    public MainPageViewModel()
    {
        MockEmails = new();
        _mockUsers = new();
    }


    [RelayCommand]
    private async Task Authenticate()
    {
        User user = _mockUsers.FirstOrDefault(u => u.Email == User.Email && u.Password == User.Password)!;
        
        if (user is not null)
        {
            await Shell.Current.GoToAsync("//home");
            return;
        }

        // Show error message
        await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");

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

        _mockUsers = new ObservableCollection<User>(userFaker.Generate(10));
            
        //map the emails to the mockEmails collection
        MockEmails = new ObservableCollection<string>(_mockUsers.Select(u => u.Email));
    }

}
