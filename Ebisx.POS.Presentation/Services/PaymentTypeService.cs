using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class PaymentTypeService : IPaymentTypeService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly JsonSerializerOptions _serializerOptions;

    public PaymentTypeService(IMapper mapper)
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

    public async Task<List<PaymentType>> GetPaymentTypesAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/PaymentType");
            if (response.IsSuccessStatusCode)
            {
                var paymentTypesResponse = await response.Content.ReadFromJsonAsync<List<PaymentTypeResponseDto>>(_serializerOptions)
                                           ?? new();
                return _mapper.Map<List<PaymentType>>(paymentTypesResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<PaymentType> GetPaymentTypeByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"api/PaymentType/{id}");
            if (response.IsSuccessStatusCode)
            {
                var paymentTypeResponse = await response.Content.ReadFromJsonAsync<PaymentTypeResponseDto>(_serializerOptions)
                                          ?? new();
                return _mapper.Map<PaymentType>(paymentTypeResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<bool> CreatePaymentTypeAsync(PaymentType paymentType)
    {
       
        try
        {
            
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<PaymentRequestDto>(paymentType), _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/PaymentType", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeletePaymentTypeAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/PaymentType/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
