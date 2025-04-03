namespace Ebisx.POS.Presentation.Views;

public partial class PrintSalesInvoicePage : ContentPage
{
    private readonly PrintSalesInvoicePageViewModel _vm;

    public PrintSalesInvoicePage(PrintSalesInvoicePageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.InitializeValues(); 
    }
}