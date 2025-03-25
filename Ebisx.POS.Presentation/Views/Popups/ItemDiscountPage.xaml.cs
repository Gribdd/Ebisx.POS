using CommunityToolkit.Maui.Views;

namespace Ebisx.POS.Presentation.Views.Popups;

public partial class ItemDiscountPage : Popup
{
    public ItemDiscountPage(ItemDiscountPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}