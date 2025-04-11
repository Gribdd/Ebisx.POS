using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Ebisx.POS.Presentation.Models;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class SalesInvoiceService : ISalesInvoiceService
{
    private readonly HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    public SalesInvoiceService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    public async Task<List<SalesInvoice>> GetSalesInvoicesAsync()
    {
        var salesInvoices = new List<SalesInvoice>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/SalesInvoice");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                salesInvoices = JsonSerializer.Deserialize<List<SalesInvoice>>(content, _serializerOptions) ?? new List<SalesInvoice>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return salesInvoices;
    }

    public async Task<SalesInvoice> GetSalesInvoiceByIdAsync(int id)
    {
        SalesInvoice salesInvoice = null;
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"api/SalesInvoice/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                salesInvoice = JsonSerializer.Deserialize<SalesInvoice>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return salesInvoice;
    }

    public async Task<bool> CreateSalesInvoiceAsync(SalesInvoice salesInvoice)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(salesInvoice, _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/SalesInvoice", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteSalesInvoiceAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/SalesInvoice/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
