namespace Ebisx.POS.Presentation.Views;

public partial class ItemInventoryPage : ContentPage
{
    ItemInventoryPageViewModel _vm;
    public ItemInventoryPage(ItemInventoryPageViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
		BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadProducts();
        _vm.SelectedProduct = _vm.Products[0];
    }
}