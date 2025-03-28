using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class ItemDiscountPageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    public double PopupWidth => Shell.Current.CurrentPage.Width * 0.7;
    public double PopupHeight => Shell.Current.CurrentPage.Width * 0.2;
    [ObservableProperty]
    public partial string CouponCode { get; set; }
    public ItemDiscountPageViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    private void Close()
    {
        _popupService.ClosePopup();
    }

    [RelayCommand]
    private async Task ConsumeCoupon()
    {
        // Call the API to validate the coupon code
        bool isCouponValid = true;
        if (!isCouponValid)
        {
            await Shell.Current.DisplayAlert("Error","Invalid coupon code", "OK");
            return;

        }
        decimal discountAmount = 10;
        await _popupService.ClosePopupAsync(discountAmount);
    }
}
