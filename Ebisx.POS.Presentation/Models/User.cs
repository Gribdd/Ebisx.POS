
namespace Ebisx.POS.Presentation.Models;

public class User
{
    public string PublicId { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime BirthDate { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public UserRole? UserRole { get; set; }
}

