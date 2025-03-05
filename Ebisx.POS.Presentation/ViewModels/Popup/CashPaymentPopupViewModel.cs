
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class CashPaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    
    public View? Anchor { get; set; } 

    public CashPaymentPopupViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    private void Back()
    {
        _popupService.ClosePopup();
    }
}
