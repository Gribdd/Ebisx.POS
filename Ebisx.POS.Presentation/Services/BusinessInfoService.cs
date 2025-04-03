using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Ebisx.POS.Presentation.Models;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class BusinessInfoService : IBusinessInfoService
{
    private readonly HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    public BusinessInfoService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    public async Task<List<BusinessInfo>> GetBusinessInfosAsync()
    {
        var businessInfos = new List<BusinessInfo>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/BusinessInfo");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                businessInfos = JsonSerializer.Deserialize<List<BusinessInfo>>(content, _serializerOptions) ?? new List<BusinessInfo>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return businessInfos;
    }

    public async Task<BusinessInfo> GetBusinessInfoByIdAsync(int id)
    {
        BusinessInfo businessInfo = null;
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/BusinessInfo/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                businessInfo = JsonSerializer.Deserialize<BusinessInfo>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return businessInfo;
    }

    public async Task<bool> CreateBusinessInfoAsync(BusinessInfo businessInfo)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(businessInfo, _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/BusinessInfo", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteBusinessInfoAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/BusinessInfo/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
