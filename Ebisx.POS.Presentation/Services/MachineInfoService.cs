using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Ebisx.POS.Presentation.Models;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class MachineInfoService : IMachineInfoService
{
    private readonly HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    public MachineInfoService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    public async Task<List<MachineInfo>> GetMachineInfosAsync()
    {
        var machineInfos = new List<MachineInfo>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/MachineInfo");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                machineInfos = JsonSerializer.Deserialize<List<MachineInfo>>(content, _serializerOptions) ?? new List<MachineInfo>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return machineInfos;
    }

    public async Task<MachineInfo> GetMachineInfoByIdAsync(int id)
    {
        MachineInfo machineInfo = null;
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/MachineInfo/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                machineInfo = JsonSerializer.Deserialize<MachineInfo>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return machineInfo;
    }


    public async Task<bool> CreateMachineInfoAsync(MachineInfo machineInfo)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(machineInfo, _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/MachineInfo", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteMachineInfoAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/MachineInfo/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
