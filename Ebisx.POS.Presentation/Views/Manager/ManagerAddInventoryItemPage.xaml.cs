using Ebisx.POS.Presentation.ViewModels.Manager;

namespace Ebisx.POS.Presentation.Views.Manager;

public partial class ManagerAddInventoryItem : ContentPage
{
    private readonly ManagerInventoryAddItemPageViewModel _vm;

    public ManagerAddInventoryItem(ManagerInventoryAddItemPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}   