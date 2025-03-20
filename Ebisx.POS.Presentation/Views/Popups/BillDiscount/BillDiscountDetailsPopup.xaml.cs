using CommunityToolkit.Maui.Views;
using Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount;

namespace Ebisx.POS.Presentation.Views.Popups.BillDiscount;

public partial class BillDiscountDetailsPopup : Popup
{
    private readonly BillDiscountDetailsPopupViewModel vm;

    public BillDiscountDetailsPopup(BillDiscountDetailsPopupViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }
}