
using System.Windows.Input;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup;

public partial class PaymentPopupViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    [ObservableProperty]
    public partial ObservableCollection<PaymentMethod> PaymentMethods { get; set; }
    public ICommand AddPaymentCommand { get; }

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

        AddPaymentCommand = new Command<PaymentMethod>(AddPayment);
        _popupService = popupService;
    }

    private void AddPayment(PaymentMethod paymentMethod)
    {
        // Handle payment logic here
        Console.WriteLine($"Adding payment for: {paymentMethod.Name}");
    }

    [RelayCommand]
    void OnCancel()
    {
        _popupService.ClosePopup();
    }

}
