using CommunityToolkit.Maui.Views;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.Services;

public class PopupService : IPopupService
{
    public void ShowPopup(Popup popup)
    {
        Page page = Shell.Current.CurrentPage ?? throw new NullReferenceException();
        page.ShowPopup(popup);
    }
}
