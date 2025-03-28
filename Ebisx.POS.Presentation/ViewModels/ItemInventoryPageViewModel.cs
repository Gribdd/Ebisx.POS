
using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class ItemInventoryPageViewModel : BaseViewModel
{
    private readonly IProductService _productService;

    [ObservableProperty]
    public partial ObservableCollection<Product> Products { get; set; } = new();

    [ObservableProperty]
    public partial Product SelectedProduct { get; set; } = new();
    [ObservableProperty]
    public partial ObservableCollection<Product> FilteredProducts { get; set; } = new();

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged();
                FilterProducts(); // Call filtering logic when search text changes
            }
        }
    }

    public ItemInventoryPageViewModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task LoadProducts()
    {
        Products = new ObservableCollection<Product>(await _productService.GetProductsAsync());
        FilterProducts();
    }

    [RelayCommand]
    private async Task NavigateBack(Product selectedProduct)
    {
        // mapped product to order item
        var orderItem = new OrderItem
        {
            ProductId = selectedProduct.Id,
            ProductName = selectedProduct.Name,
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

    private void FilterProducts()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredProducts = new ObservableCollection<Product>(Products);
        }
        else
        {
            var filtered = Products
                .Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            p.Barcode.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredProducts = new ObservableCollection<Product>(filtered);
        }
    }

}
