
namespace Ebisx.POS.Presentation.ViewModels;

public partial class ItemInventoryPageViewModel : BaseViewModel
{
    private readonly MockDataService _service;

    [ObservableProperty]
    private ObservableCollection<Product> _products = new();

    public ItemInventoryPageViewModel(MockDataService service)
    {
        _service = service;
    }

    public async Task LoadProducts()
    {
        Products = await _service.LoadProductsAsync();
    }

}
