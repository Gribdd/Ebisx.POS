using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace Ebisx.POS.Presentation.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IMapper _mapper;

    public PaymentService(IMapper mapper)
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

    public async Task<List<Payment>> GetPaymentsAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Payment");
            if (response.IsSuccessStatusCode)
            {
                var paymentsResponse = await response.Content.ReadFromJsonAsync<List<PaymentResponseDto>>(_serializerOptions) ?? new();
                return _mapper.Map<List<Payment>>(paymentsResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"api/Payment/{id}");
            if (response.IsSuccessStatusCode)
            {
                var paymentResponse = await response.Content.ReadFromJsonAsync<Payment>(_serializerOptions) ?? new();
                return _mapper.Map<Payment>(paymentResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<Payment> CreatePaymentAsync(Payment paymentRequest)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<PaymentRequestDto>(paymentRequest), _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Payment", content);
            if (response.IsSuccessStatusCode)
            {
                var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponseDto>(_serializerOptions) ?? new();
               
                return _mapper.Map<Payment>(paymentResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR from PaymentService {0}", ex.Message);
        }

        return new();
    }

    public async Task<bool> DeletePaymentAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Payment/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

}
