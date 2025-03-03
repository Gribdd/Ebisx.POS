
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class ItemInventoryPageViewModel : BaseViewModel
{
    private readonly MockDataService _service;

    [ObservableProperty]
    public partial ObservableCollection<Product> Products { get; set; } = new();

    public ItemInventoryPageViewModel(MockDataService service)
    {
        _service = service;
    }

    public async Task LoadProducts()
    {
        Products = await _service.LoadProductsAsync();
    }

    [RelayCommand]
    private async Task NavigateBack(Product selectedProduct)
    {
        // mapped product to order item
        var orderItem = new OrderItem
        {
            ProductId = selectedProduct.Id,
            ProductName = selectedProduct.ProductName,
            QuantityAtPurchase = 1,
            PriceAtPurchase = selectedProduct.Price,
            VatAtPurchase = selectedProduct.Vat,            
            DiscountPercentage = 0,
            Barcode = selectedProduct.Barcode
        };

        var navigationParameter = new Dictionary<string,object>
        {
            { nameof(OrderItem), orderItem }
        };

        await Shell.Current.GoToAsync("..",navigationParameter);
    }

}
