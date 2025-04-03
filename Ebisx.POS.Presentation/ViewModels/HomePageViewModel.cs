
using CommunityToolkit.Maui.Core;
using Ebisx.POS.Presentation.Common.Enums;

namespace Ebisx.POS.Presentation.ViewModels;

[QueryProperty(nameof(OrderItem), nameof(OrderItem))]
public partial class HomePageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly ISettingsService _settingService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public partial ObservableCollection<OrderItem> OrderItems { get; set; } = new();
    
    [ObservableProperty]
    public partial OrderItem OrderItem { get; set; } = new();
    
    [ObservableProperty]
    public partial KeypadMode CurrentMode { get; set; } = KeypadMode.None;
    
    [ObservableProperty]
    public partial bool IsInputActive { get; set; } = false;

    [ObservableProperty]
    public partial int TotalQuantity { get; set; }
    
    [ObservableProperty]
    public partial int TotalNumOfItems { get; set; }
    
    [ObservableProperty]
    public partial decimal TotalDiscount { get; set; }
    
    [ObservableProperty]
    public partial string? SelectedNumber { get; set; }
        
    [ObservableProperty]
    public partial decimal GrandTotal { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ChangeAmount))]
    public partial decimal TotalAmountPaid { get; set; }

    public decimal ChangeAmount => TotalAmountPaid - GrandTotal;

    private OrderItem? _selectedOrderItem = new();

    public OrderItem? SelectedOrderItem
    {
        get { return _selectedOrderItem; }
        set 
        {
            if (Equals(value, _selectedOrderItem))
            {
                _selectedOrderItem = null;
            }
            else
            {
                _selectedOrderItem = value;
            }
            OnPropertyChanged(nameof(SelectedOrderItem));
        }
    }


    public HomePageViewModel(
        IPopupService popupService,
        ISettingsService settingsService,
        INavigationService navigationService)
    {
        _popupService = popupService;
        _settingService = settingsService;
        _navigationService = navigationService;
    }

    private void CalculateTotalQuantity()
    {
        TotalQuantity = OrderItems.Sum(o => o.QuantityAtPurchase);
    }

    private void CalculateTotalNumOfItems()
    {
        TotalNumOfItems = OrderItems.Count;
    }

    private void CalculateGrandTotal()
    {
        GrandTotal = OrderItems.Sum(o => o.TotalAmount);
    }

    private void CalculateTotalDiscount()
    {
        TotalDiscount = OrderItems.Sum(o => o.DiscountAmount);
    }

    private void CalculateTotals()
    {
        CalculateTotalQuantity();
        CalculateTotalNumOfItems();
        CalculateGrandTotal();
        CalculateTotalDiscount();
    }

    public void UpdateOrderItems()
    {
        if (OrderItem != null && !string.IsNullOrEmpty(OrderItem.ProductName))
        {
            OrderItems.Add(OrderItem);
            CalculateTotals();
        }
    }

    [RelayCommand]
    private async Task NavigateToItemSearch()
    {
        await _navigationService.NavigateToAsync($"//{AppRoutes.ItemInventory}");
    }

    [RelayCommand]
    private void DeleteSelectedOrderItem()
    {
        if (SelectedOrderItem != null)
        {
            OrderItems.Remove(SelectedOrderItem);
            SelectedOrderItem = null; // Reset selection after deletion
            CalculateTotals();
        }
    }
        
    [RelayCommand]
    private void NewTransaction()
    {
        OrderItems.Clear();
        CalculateTotals();
    }

    [RelayCommand]
    private void HoldTransaction()
    { 

        //save transaction
        Shell.Current.DisplayAlert(
            "Success", 
            "Transaction held and saved successfully.", 
            "OK");
        
        //start new transaction
        NewTransaction();
    }

    [RelayCommand]
    private void ChangeOrderItemQuantity()
    {
        if (SelectedOrderItem == null)
        {
            Shell.Current.DisplayAlert
                ("Error", 
                "Please select an item first.",
                "OK");
            return;
        }

        
        if (CurrentMode == KeypadMode.ChangeQuantity)
        {
            // Exiting mode
            CurrentMode = KeypadMode.None;
            IsInputActive = false;
            CalculateTotals();
            SelectedNumber = string.Empty;
        }
        else
        {
            // Entering mode
            CurrentMode = KeypadMode.ChangeQuantity;
            IsInputActive = true;
        }
    }

    [RelayCommand]
    private async Task ApplyItemDiscount()
    {
        if (SelectedOrderItem == null)
        {
            await Shell.Current.DisplayAlert
                ("Error", 
                "Please select an item first.", 
                "OK");
            return;
        }

        var discountPercentage = await _popupService.ShowPopupAsync<ItemDiscountPageViewModel>();
        if (discountPercentage == null)
        {
            await Shell.Current.DisplayAlert
                ("Not Found",
                "Coupon not Found.",
                "OK");
            return;
        }

        SelectedOrderItem.DiscountPercentage = (decimal)discountPercentage;
    }

    [RelayCommand]      
    private async Task ApplyBillDiscount()
    {
        var result = await _popupService.ShowPopupAsync<BillDiscountPopupViewModel>();

        if (result != null)
        {
            var discountType = result.ToString() ?? string.Empty;
            var isBillDiscounted = await _popupService.ShowPopupAsync<BillDiscountDetailsPopupViewModel>(
                onPresenting: vm => 
                    vm.DiscountType =  discountType);

            if ( (bool) isBillDiscounted!)
            {
                // apply discount
                foreach (var orderItem in OrderItems)
                {
                    // apply 20% discount 
                    orderItem.DiscountPercentage += 5;
                }
            }

        }
    }

    [RelayCommand]
    private void NumberInput(string input)
    {
        // base case
        if (!IsInputActive) return;

        SelectedNumber += input;

        if (decimal.TryParse(SelectedNumber, out decimal value))
        {
            switch (CurrentMode)
            {
                case KeypadMode.ChangeQuantity:
                    if (SelectedOrderItem != null)
                        SelectedOrderItem.QuantityAtPurchase = (int)value;
                    break;

                case KeypadMode.ItemDiscount:
                    if (SelectedOrderItem != null)
                        SelectedOrderItem.DiscountPercentage = (decimal)value;
                    break;
            }
        }
            
    }

    [RelayCommand]
    private void CloseTally()
    {
        Shell.Current.DisplayAlert
            ("Tally Closed",
            "CANNOT CLOSE TALLY. CALL A MANAGER.", 
            "OK");
    }

    [RelayCommand]
    private async Task ProcessTenderFloat()
    {
        // cannot proceed if order items length is 0
        if (OrderItems.Count == 0)
        {
            await Shell.Current.DisplayAlert
                ("Error",
                "No items found in transaction.",
                "OK");
            return;
        }

        var result = await _popupService.ShowPopupAsync<PaymentPopupViewModel>();

        if (result == null) return;

        decimal amountPaid = 0;
        switch (result)
        {
            case "Cash":
                amountPaid = (decimal?)await _popupService.ShowPopupAsync<CashPaymentPopupViewModel>() ?? 0;
                break;
        }

        TotalAmountPaid = amountPaid;
            
    }


    [RelayCommand]
    private void Logout()
    {
        _navigationService.Logout();
        _settingService.Logout();
    }

    [RelayCommand]
    private async Task PrintInvoice()
    {
        await _navigationService.NavigateToAsync(AppRoutes.PrintInvoice);
    }

}
