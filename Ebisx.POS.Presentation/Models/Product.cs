namespace Ebisx.POS.Presentation.Models;

public partial class Product : BaseModel
{
    [ObservableProperty]
    private string? _productName;

    [ObservableProperty]
    private string? _barcode;

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private decimal _vat;
    
    [ObservableProperty]
    private string? _salesUnit;
}
