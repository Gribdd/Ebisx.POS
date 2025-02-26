namespace Ebisx.POS.Presentation.Models;

public partial class User : BaseModel
{
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private DateTime _birthDate;

    public User()
    {
        _username = string.Empty;
        _password = string.Empty;
        _email = string.Empty;
    }
}
