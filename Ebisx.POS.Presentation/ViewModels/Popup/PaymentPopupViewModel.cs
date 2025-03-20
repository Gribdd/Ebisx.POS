
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class PaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    [ObservableProperty]
    public partial ObservableCollection<PaymentMethod> PaymentMethods { get; set; }

    public double PopupWidth => Shell.Current.CurrentPage.Width * 0.9;
    public double PopupHeight => Shell.Current.CurrentPage.Height * 0.9;

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
        if (parameter is Tuple<object, object> tuple &&
            tuple.Item1 is PaymentMethod paymentMethod &&
            tuple.Item2 is Button anchorButton)
        {
                

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = $"Payment added: {paymentMethod.Name} with an amount of PHP {paymentMethod.Amount}";
            ToastDuration duration = ToastDuration.Short;   
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);


            switch (paymentMethod.Name)
            {
                case "Cash":
                    _popupService.ShowPopup<CashPaymentPopupViewModel>(
                        onPresenting: vm => vm.Anchor = anchorButton);
                    break;
                case "Credit":
                    // Handle credit payment
                    break;
            }
        }
    }


    [RelayCommand]
    private void HandlePayment()
    {
        _popupService.ClosePopup();
    }

}
