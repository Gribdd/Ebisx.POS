using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Ebisx.POS.Presentation.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    JsonSerializerOptions _serializerOptions;

    public ProductService(IMapper mapper)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        _mapper = mapper;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Product");
            if (response.IsSuccessStatusCode)
            {
                var productResponse = await response.Content.ReadFromJsonAsync<List<ProductResponseDto>>(_serializerOptions) ?? new();
                return _mapper.Map<List<Product>>(productResponse);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return new();
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        if (product == null)
        {
            return false;
        }

        try
        {
            var productRequest = _mapper.Map<ProductRequestDto>(product);

            string jsonContent = JsonSerializer.Serialize(productRequest, _serializerOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Product", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Product/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return false;
    }
}
