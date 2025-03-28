using System.Text.Json.Serialization;

namespace Ebisx.POS.Presentation.Models;

public partial class Product : BaseModel
{
    [ObservableProperty]
    [JsonPropertyName("name")]
    public partial string? Name { get; set; }

    [ObservableProperty]
    [JsonPropertyName("barcode")]
    public partial string? Barcode { get; set; }

    [ObservableProperty]
    [JsonPropertyName("quantity")]
    public partial int Quantity { get; set; }

    [ObservableProperty]
    [JsonPropertyName("price")]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    [JsonPropertyName("vat")]
    public partial decimal Vat { get; set; }
    
    [ObservableProperty]
    [JsonPropertyName("salesUnit")]
    public partial string? SalesUnit { get; set; }

}
