using CommunityToolkit.Maui.Views;
namespace Ebisx.POS.Presentation.Views.Popups;

public partial class PaymentPopup : Popup
{
    private readonly PaymentPopupViewModel _vm;

    public PaymentPopup(PaymentPopupViewModel vm)	
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}
}