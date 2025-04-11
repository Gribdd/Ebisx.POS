
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
        _mapper = mapper;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/User");
            if (response.IsSuccessStatusCode)
            {
                var usersResponse = await response.Content.ReadFromJsonAsync<List<UserResponseDto>>(_serializerOptions)
                                    ?? new();

                return _mapper.Map<List<User>>(usersResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                var userResponse = await response.Content.ReadFromJsonAsync<UserResponseDto>(_serializerOptions) ?? new();
                return _mapper.Map<User>(userResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }


    public async Task<bool> CreateUserAsync(User userRequest)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<UserRequestDto>(userRequest), _serializerOptions);
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
