namespace Ebisx.POS.Presentation.Models;

public partial class Product : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    public partial string? Name { get; set; }

    [ObservableProperty]
    public partial string? Barcode { get; set; }

    [ObservableProperty]
    public partial int Quantity { get; set; }

    [ObservableProperty]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    public partial decimal Vat { get; set; }
    
    [ObservableProperty]
    public partial string? SalesUnit { get; set; }

}
