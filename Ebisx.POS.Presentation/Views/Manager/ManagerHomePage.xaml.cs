using Ebisx.POS.Presentation.ViewModels.Manager;

namespace Ebisx.POS.Presentation.Views.Manager;

public partial class ManagerHomePage : ContentPage
{
	public ManagerHomePage(ManagerHomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}