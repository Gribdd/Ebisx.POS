namespace Ebisx.POS.Presentation.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _vm;

    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.UpdateOrderItems();
        _vm.LoadPaymentMethodsAvailable();
    }
}