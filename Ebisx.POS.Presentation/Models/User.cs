namespace Ebisx.POS.Presentation.Models;

public partial class User : BaseModel
{
    [ObservableProperty]
    public partial string Username { get; set; }

    [ObservableProperty]
    public partial string Password { get; set; }

    [ObservableProperty]
    public partial string Email { get; set; }

    [ObservableProperty]
    public partial DateTime BirthDate { get; set; }

    public UserRole UserRole { get; set; }
}
    