using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class ItemDiscountPageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    public ItemDiscountPageViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    private void Close()
    {
        _popupService.ClosePopup();
    }
}
