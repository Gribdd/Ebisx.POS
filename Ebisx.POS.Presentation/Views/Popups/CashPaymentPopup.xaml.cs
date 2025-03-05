using CommunityToolkit.Maui.Views;

namespace Ebisx.POS.Presentation.Views.Popups;

public partial class CashPaymentPopup : Popup
{
	public CashPaymentPopup(CashPaymentPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}