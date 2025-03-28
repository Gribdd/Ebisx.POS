
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class CashPaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    // Cash Notes
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal ThousandNote { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal FiveHundredNote { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal TwoHundredNote { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal HundredNote { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal FiftyNote { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCashNotes))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal TwentyNote { get; set; }

    // Coins
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCoins))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal TenCoin { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCoins))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal FiveCoin { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCoins))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal OneCoin { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCoins))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal FiftyCentsCoin { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalCoins))]
    [NotifyPropertyChangedFor(nameof(GrandTotal))]
    public partial decimal QuarterCoin { get; set; }

    // Total amount for Cash Notes
    public decimal TotalCashNotes =>
        (ThousandNote * 1000) + (FiveHundredNote * 500) + (TwoHundredNote * 200) +
        (HundredNote * 100) + (FiftyNote * 50) + (TwentyNote * 20);

    // Total amount for Coins
    public decimal TotalCoins =>
        (TenCoin * 10m) + (FiveCoin * 5m) + (OneCoin * 1m) + (FiftyCentsCoin * 0.50m) + (QuarterCoin * 0.25m);


    // Grand Total (Cash Notes + Coins)
    public decimal GrandTotal => TotalCashNotes + TotalCoins;

    public CashPaymentPopupViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    private void Back()
    {
        _popupService.ClosePopup();
    }

    [RelayCommand]
    private void Reset()
    {
        ThousandNote = 0;
        FiveHundredNote = 0;
        TwoHundredNote = 0;
        HundredNote = 0;
        FiftyNote = 0;
        TwentyNote = 0;
        TenCoin = 0;
        FiveCoin = 0;
        OneCoin = 0;
        FiftyCentsCoin = 0;
        QuarterCoin = 0;
    }

    [RelayCommand]
    private async Task Proceed()
    {
        await _popupService.ClosePopupAsync();
        await _popupService.ShowPopupAsync<PaymentPopupViewModel>(
            onPresenting: vm => vm.CashAmount = GrandTotal);
    }
}
