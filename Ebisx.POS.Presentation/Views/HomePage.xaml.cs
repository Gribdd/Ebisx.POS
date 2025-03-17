namespace Ebisx.POS.Presentation.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel vm;

    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.UpdateOrderItems();
    }
}