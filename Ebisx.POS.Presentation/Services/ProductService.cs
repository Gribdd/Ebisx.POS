using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    public ProductService() 
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.BaseAddress);
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var _products = new List<Product>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress+"api/Product");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                _products = JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions) ?? new List<Product>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return _products;
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        try
        {
            var productToSend = new
            {
                Name = product.Name,
                Barcode = product.Barcode,
                Quantity = product.Quantity,
                Price = product.Price,
                Vat = product.Vat,
                SalesUnit = product.SalesUnit
            };

            string jsonContent = JsonSerializer.Serialize(productToSend, _serializerOptions);
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

    public async Task<bool> DeleteProductAsync(Guid id)
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
