namespace Ebisx.POS.Presentation.Views;

public partial class HomePage : ContentPage
{
    HomePageViewModel _vm;
    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.UpdateOrderItems();
    }
}