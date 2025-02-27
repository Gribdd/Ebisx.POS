namespace Ebisx.POS.Presentation.Models;

public partial class OrderItem : BaseModel
{
    [ObservableProperty]
    public partial int ProductId { get; set; } // Reference to Product

    [ObservableProperty]
    public partial string? ProductName { get; set; } // Store Name for display

    [ObservableProperty]
    public partial string? Barcode { get; set; } // Store Barcode for quick lookup

    [ObservableProperty]
    public partial int QuantityAtPurchase { get; set; }

    [ObservableProperty]
    public partial decimal PriceAtPurchase { get; set; } // Store price at the time of purchase

    [ObservableProperty]
    public partial decimal VatAtPurchase { get; set; }

    [ObservableProperty]
    public partial decimal Discount { get; set; }

    public decimal TotalAmount => ((PriceAtPurchase * QuantityAtPurchase) - Discount) * (1 + (VatAtPurchase / 100));
}
