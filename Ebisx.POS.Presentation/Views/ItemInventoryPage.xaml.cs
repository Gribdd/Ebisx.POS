namespace Ebisx.POS.Presentation.Views;

public partial class ItemInventoryPage : ContentPage
{
    ItemInventoryPageViewModel _vm;
    public ItemInventoryPage(ItemInventoryPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadProducts();
    }
}