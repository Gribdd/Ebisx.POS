using CommunityToolkit.Maui.Views;
namespace Ebisx.POS.Presentation.Views.Popups;

public partial class PaymentPopup : Popup
{
	public PaymentPopup(PaymentPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}