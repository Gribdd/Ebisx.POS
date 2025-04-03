namespace Ebisx.POS.Presentation.Services.Interface;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<bool> CreateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}
