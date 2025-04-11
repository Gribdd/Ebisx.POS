using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IMapper _mapper;

    public OrderService(IMapper mapper)
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

    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Order");
            if (response.IsSuccessStatusCode)
            {
                var ordersResponse = await response.Content.ReadFromJsonAsync<List<OrderResponseDto>>(_serializerOptions)
                                     ?? new();
                return _mapper.Map<List<Order>>(ordersResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"api/Order/{id}");
            if (response.IsSuccessStatusCode)
            {
                var orderResponse = await response.Content.ReadFromJsonAsync<OrderResponseDto>(_serializerOptions)
                                    ?? new();
                return _mapper.Map<Order>(orderResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<Order> CreateOrderAsync(Order orderRequest)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<OrderRequestDto>(orderRequest), _serializerOptions);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Order", content);

            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<OrderResponseDto>(_serializerOptions) ?? new();
                return _mapper.Map<Order>(order);
            }
            else
            { 
                string errorResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Error Response: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"\tERROR from Order Service: {ex.Message}");
        }

        return new();
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Order/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> UpdateOrderAsync(Order orderRequest)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<OrderRequestDto>(orderRequest), _serializerOptions);

            Debug.WriteLine("JSON Payload: " + jsonContent);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"api/Order/{orderRequest.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Error Response: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"\tERROR: {ex.Message}");
        }

        return false;
    }

    public async Task<Order> AddItemsToOrder(int orderId, List<OrderItem> newItems)
    {
        try
        {
            string jsonContent = JsonSerializer.Serialize(_mapper.Map<List<OrderItemRequestDto>>(newItems), _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"api/Order/{orderId}/items", content);

            if (response.IsSuccessStatusCode)
            {
                var orderResponse = await response.Content.ReadFromJsonAsync<OrderResponseDto>(_serializerOptions)
                                        ?? new();
                return _mapper.Map<Order>(orderResponse);
            }
            else
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Error Response: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"\tERROR in AddItemsToOrderAsync: {ex.Message}");
        }

        return new();
    }
}
