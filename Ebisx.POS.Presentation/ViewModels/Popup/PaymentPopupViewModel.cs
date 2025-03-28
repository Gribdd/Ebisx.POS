
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class PaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    [ObservableProperty]
    public partial ObservableCollection<PaymentMethod> PaymentMethods { get; set; }

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
        PaymentMethods =
        [
            new () { Name = "Cash", Amount = 0 },
            new () { Name = "Credit", Amount = 0 },
            new () { Name = "Debit", Amount = 0 },
            new () { Name = "E-Pay", Amount = 0 },
            new () { Name = "Customer Credit", Amount = 0 },
            new () { Name = "Loyalty Card", Amount = 0 },
        ];

        _popupService = popupService;
    }

    [RelayCommand]
    private void Close()
    {
        _popupService.ClosePopup();
    }

    [RelayCommand]
    private async Task AddPayment(object parameter)
    {

        switch (parameter)
        {
            case "Cash":
                await  _popupService.ClosePopupAsync();
                await _popupService.ShowPopupAsync<CashPaymentPopupViewModel>();
                break;
            case "Credit":
                // Handle credit payment
                break;
        }
    }


    [RelayCommand]
    private void HandlePayment()
    {
        _popupService.ClosePopup();
    }

}
