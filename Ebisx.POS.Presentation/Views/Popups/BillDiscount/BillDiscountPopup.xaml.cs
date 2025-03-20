using CommunityToolkit.Maui.Views;
using Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount;

namespace Ebisx.POS.Presentation.Views.Popups.BillDiscount;

public partial class BillDiscountPopup : Popup
{
    private readonly BillDiscountPopupViewModel vm;

    public BillDiscountPopup(BillDiscountPopupViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }
}