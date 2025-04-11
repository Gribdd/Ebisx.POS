namespace Ebisx.POS.Presentation.DTOs.Product;

// Product DTOs
public class ProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Vat { get; set; }
    public string SalesUnit { get; set; } = string.Empty;
}
