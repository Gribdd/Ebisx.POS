namespace Ebisx.POS.Presentation.Views;

public partial class PrintSalesInvoicePage : ContentPage
{
	public PrintSalesInvoicePage(PrintSalesInvoicePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}