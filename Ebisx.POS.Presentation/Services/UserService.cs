
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    public UserService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var _users = new List<User>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/User");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                _users = JsonSerializer.Deserialize<List<User>>(content, _serializerOptions) ?? new List<User>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return _users;
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        try
        {
            var userToSend = new
            {
                Fname = user.FName,
                Lname = user.LName,
                EmailAddress = user.EmailAddress,
                Address = user.Address,
                BirthDate = user.BirthDate,
                Username = user.Username,
                Password = user.Password,
                RoleId = user.RoleId
            };

            string jsonContent = JsonSerializer.Serialize(userToSend, _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/User", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/User/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
