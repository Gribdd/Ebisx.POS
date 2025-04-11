using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class OrderItemService : IOrderItemService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IMapper _mapper;

    public OrderItemService(IMapper mapper)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(Constants.BaseAddress) };
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
        _mapper = mapper;
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync()
    {
        var response = await _httpClient.GetAsync("api/OrderItem");
        if (response.IsSuccessStatusCode)
        {
            var dtoList = await response.Content.ReadFromJsonAsync<List<OrderItemResponseDto>>(_serializerOptions) ?? new();
            return _mapper.Map<List<OrderItem>>(dtoList);
        }
        return new();
    }

    public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/OrderItem/{id}");
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<OrderItemResponseDto>(_serializerOptions) ?? new();
            return _mapper.Map<OrderItem>(dto);
        }
        return null;
    }

    public async Task<OrderItem?> CreateOrderItemAsync(OrderItem orderItem)
    {
        var content = new StringContent(JsonSerializer.Serialize(_mapper.Map<OrderItemRequestDto>(orderItem), _serializerOptions),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/OrderItem", content);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<OrderItemResponseDto>(_serializerOptions) ?? new();
            return _mapper.Map<OrderItem>(dto);
        }

        return null;
    }

    public async Task<bool> UpdateOrderItemAsync(int id, OrderItem orderItem)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(_mapper.Map<OrderItemRequestDto>(orderItem), _serializerOptions),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync($"api/OrderItem/{id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteOrderItemAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/OrderItem/{id}");
        return response.IsSuccessStatusCode;
    }
}

