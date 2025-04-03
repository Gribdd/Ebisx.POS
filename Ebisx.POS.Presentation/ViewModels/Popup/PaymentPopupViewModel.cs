
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class PaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal CashAmount { get; set; }
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal CreditAmount { get; set; }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal DebitAmount { get; set; }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal EpayAmount { get; set; }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal CustomerCreditAmount { get; set; }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalAmountPaid))]
    public partial decimal LoyaltyCardAmount { get; set; }
    public decimal TotalAmountPaid =>
        CashAmount + CreditAmount + DebitAmount + EpayAmount + CustomerCreditAmount + LoyaltyCardAmount;

    public double PopupWidth => Shell.Current.CurrentPage.Width;
    public double PopupHeight => Shell.Current.CurrentPage.Height;


    public PaymentPopupViewModel(IPopupService popupService)
    {
       
        _popupService = popupService;
    }

    [RelayCommand]
    private async Task Close()
    {
        if(CashAmount > 0)
        {
            await _popupService.ClosePopupAsync(CashAmount);
            return;
        }
        else
        {
            _popupService.ClosePopup();
        }
    }

    [RelayCommand]
    private async Task AddPayment(object parameter)
    {

        switch (parameter)  
        {
            case "Cash":
                await _popupService.ClosePopupAsync(parameter);
                break;
            case "Credit":
                // Handle credit payment
                break;
        }
    }


    [RelayCommand]
    private async Task HandlePayment()
    {
        if (CashAmount > 0)
        {
            await _popupService.ClosePopupAsync(CashAmount);
            return;
        }
        else
        {
            _popupService.ClosePopup();
        }
    }
}
