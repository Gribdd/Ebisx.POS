namespace Ebisx.POS.Presentation.Models;

public partial class OrderItem : BaseModel
{
    [ObservableProperty]
    public partial Guid ProductId { get; set; } // Reference to Product

    [ObservableProperty]
    public partial string? ProductName { get; set; } // Store Name for display

    [ObservableProperty]
    public partial string? Barcode { get; set; } // Store Barcode for quick lookup

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmount))]
    public partial int QuantityAtPurchase { get; set; }

    [ObservableProperty]    
    public partial decimal PriceAtPurchase { get; set; } // Store price at the time of purchase

    [ObservableProperty]
    public partial decimal VatAtPurchase { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmount))]
    [NotifyPropertyChangedFor(nameof(DiscountAmount))]
    public partial decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount => (PriceAtPurchase * QuantityAtPurchase) * (DiscountPercentage / 100);

    public decimal TotalAmount => ((PriceAtPurchase * QuantityAtPurchase) * (1 + VatAtPurchase / 100)) - DiscountAmount;

    [ObservableProperty]
    public partial int OrderId { get; set; }

}
 