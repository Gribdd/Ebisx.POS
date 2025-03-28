using Ebisx.POS.Presentation.ViewModels.Manager;

namespace Ebisx.POS.Presentation.Views.Manager;

public partial class ManagerInventoryPage : ContentPage
{
    private readonly ManagerInventoryPageViewModel _vm;

    public ManagerInventoryPage(ManagerInventoryPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadProductsAsync();
    }
}