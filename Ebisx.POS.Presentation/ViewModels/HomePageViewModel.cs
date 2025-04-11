
using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using Ebisx.POS.Presentation.Common.Enums;

namespace Ebisx.POS.Presentation.ViewModels;

/// <summary>
/// ViewModel for the HomePage, managing the state and operations for the POS system.
/// </summary>
[QueryProperty(nameof(OrderItem), nameof(OrderItem))]
public partial class HomePageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly ISettingsService _settingService;
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentTypeService _paymentTypeService;
    private readonly INavigationService _navigationService;

    /// <summary>
    /// The current order being processed.
    /// </summary>
    [ObservableProperty]
    public partial Order Order { get; set; } = new();

    /// <summary>
    /// Collection of available payment types.
    /// </summary>
    [ObservableProperty]
    public partial ObservableCollection<PaymentType> PaymentTypes { get; set; } = new();

    /// <summary>
    /// The current order item being processed.
    /// </summary>
    [ObservableProperty]
    public partial OrderItem OrderItem { get; set; } = new();

    /// <summary>
    /// The current mode of the keypad.
    /// </summary>
    [ObservableProperty]
    public partial KeypadMode CurrentMode { get; set; } = KeypadMode.None;

    /// <summary>
    /// Indicates if the input is active.
    /// </summary>
    [ObservableProperty]
    public partial bool IsInputActive { get; set; } = false;

    /// <summary>
    /// Total quantity of items in the order.
    /// </summary>
    [ObservableProperty]
    public partial int TotalQuantity { get; set; }

    /// <summary>
    /// Total number of items in the order.
    /// </summary>
    [ObservableProperty]
    public partial int TotalNumOfItems { get; set; }

    /// <summary>
    /// Total discount applied to the order.
    /// </summary>
    [ObservableProperty]
    public partial decimal TotalDiscount { get; set; }

    /// <summary>
    /// The currently selected number input.
    /// </summary>
    [ObservableProperty]
    public partial string? SelectedNumber { get; set; }

    /// <summary>
    /// The grand total amount of the order.
    /// </summary>
    [ObservableProperty]
    public partial decimal GrandTotal { get; set; }

    /// <summary>
    /// Total amount paid by the customer.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ChangeAmount))]
    public partial decimal TotalAmountPaid { get; set; }

    /// <summary>
    /// The change amount to be returned to the customer.
    /// </summary>
    public decimal ChangeAmount => TotalAmountPaid - GrandTotal;

    private OrderItem? _selectedOrderItem = new();

    /// <summary>
    /// The currently selected order item.
    /// </summary>
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

    /// <summary>
    /// Constructor for HomePageViewModel.
    /// </summary>
    public HomePageViewModel(
        IPopupService popupService,
        ISettingsService settingsService,
        IOrderService orderService,
        IOrderItemService orderItemService,
        IPaymentService paymentService,
        IPaymentTypeService paymentTypeService,
        INavigationService navigationService)
    {
        _popupService = popupService;
        _settingService = settingsService;
        _orderService = orderService;
        _orderItemService = orderItemService;
        _paymentService = paymentService;
        _paymentTypeService = paymentTypeService;
        _navigationService = navigationService;
    }

    /// <summary>
    /// Calculates the total quantity of items in the order.
    /// </summary>
    private void CalculateTotalQuantity()
    {
        TotalQuantity = Order.OrderItems.Sum(o => o.QuantityAtPurchase);
    }

    /// <summary>
    /// Calculates the total number of items in the order.
    /// </summary>
    private void CalculateTotalNumOfItems()
    {
        TotalNumOfItems = Order.OrderItems.Count;
    }

    /// <summary>
    /// Calculates the grand total amount of the order.
    /// </summary>
    private void CalculateGrandTotal()
    {
        GrandTotal = Order.OrderItems.Sum(o => o.TotalAmount);
    }

    /// <summary>
    /// Calculates the total discount applied to the order.
    /// </summary>
    private void CalculateTotalDiscount()
    {
        TotalDiscount = Order.OrderItems.Sum(o => o.DiscountAmount);
    }

    /// <summary>
    /// Calculates all the totals for the order.
    /// </summary>
    private void CalculateTotals()
    {
        CalculateTotalQuantity();
        CalculateTotalNumOfItems();
        CalculateGrandTotal();
        CalculateTotalDiscount();
    }

    /// <summary>
    /// Updates the order items and recalculates totals.
    /// </summary>
    public async Task UpdateOrderItems()
    {
        if (OrderItem != null && !string.IsNullOrEmpty(OrderItem.ProductName))
        {
            Order.OrderItems.Add(OrderItem);
            CalculateTotals();
        }

        if (Order.OrderItems.Count == 1)
        {
            Order.Status = "Pending";
            Order = await _orderService.CreateOrderAsync(Order);
        }

        if (Order.OrderItems.Count > 1)
        {
            Order = await _orderService.AddItemsToOrder(Order.Id, Order.OrderItems.ToList());
        }
    }

    /// <summary>
    /// Navigates to the item search page.
    /// </summary>
    [RelayCommand]
    private async Task NavigateToItemSearch()
    {
        await _navigationService.NavigateToAsync(AppRoutes.ItemInventory);
    }

    /// <summary>
    /// Voids the selected item from the order.
    /// </summary>
    [RelayCommand]
    private void VoidItem()
    {
        if (SelectedOrderItem != null)
        {
            Order.OrderItems.Remove(SelectedOrderItem);
            SelectedOrderItem = null; // Reset selection after deletion
            CalculateTotals();
        }
    }

    /// <summary>
    /// Starts a new transaction by clearing the order items.
    /// </summary>
    [RelayCommand]
    private void NewTransaction()
    {
        Order.OrderItems.Clear();
        TotalAmountPaid = 0;
        CalculateTotals();
    }

    /// <summary>
    /// Holds the current transaction and starts a new one.
    /// </summary>
    [RelayCommand]
    private void HoldTransaction()
    {
        // Save transaction
        Shell.Current.DisplayAlert(
            "Success",
            "Transaction held and saved successfully.",
            "OK");

        // Start new transaction
        NewTransaction();
    }

    /// <summary>
    /// Changes the quantity of the selected order item.
    /// </summary>
    [RelayCommand]
    private async void ChangeOrderItemQuantity()
    {
        if (SelectedOrderItem == null)
        {
            await Shell.Current.DisplayAlert
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
            await _orderItemService.UpdateOrderItemAsync(SelectedOrderItem.Id, SelectedOrderItem);
        }
        else
        {
            // Entering mode
            CurrentMode = KeypadMode.ChangeQuantity;
            IsInputActive = true;
        }
    }

    /// <summary>
    /// Applies a discount to the selected order item.
    /// </summary>
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

    /// <summary>
    /// Applies a discount to the entire bill.
    /// </summary>
    [RelayCommand]
    private async Task ApplyBillDiscount()
    {
        var result = await _popupService.ShowPopupAsync<BillDiscountPopupViewModel>();

        if (result != null)
        {
            var discountType = result.ToString() ?? string.Empty;
            var isBillDiscounted = await _popupService.ShowPopupAsync<BillDiscountDetailsPopupViewModel>(
                onPresenting: vm =>
                    vm.DiscountType = discountType);

            if ((bool)isBillDiscounted!)
            {
                // Apply discount
                foreach (var orderItem in Order.OrderItems)
                {
                    // Apply 20% discount 
                    orderItem.DiscountPercentage += 5;
                }
            }
        }
    }

    /// <summary>
    /// Handles number input for the keypad.
    /// </summary>
    [RelayCommand]
    private void NumberInput(string input)
    {
        // Base case
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

    /// <summary>
    /// Closes the tally.
    /// </summary>
    [RelayCommand]
    private void CloseTally()
    {
        Shell.Current.DisplayAlert
            ("Tally Closed",
            "CANNOT CLOSE TALLY. CALL A MANAGER.",
            "OK");
    }

    /// <summary>
    /// Processes the tender float for the transaction.
    /// </summary>
    [RelayCommand]
    private async Task ProcessTenderFloat()
    {
        // Cannot proceed if order items length is 0
        if (Order.OrderItems.Count == 0)
        {
            await ShowErrorAsync("No items found in transaction.");
            return;
        }

        // Cannot proceed if total amount is 0
        if (GrandTotal == 0)
        {
            await ShowErrorAsync("No amount to pay.");
            return;
        }

        // Amount paid is greater than grand total
        if (TotalAmountPaid > GrandTotal)
        {
            await ShowErrorAsync("Amount paid is greater than grand total.");
            return;
        }

        var result = await _popupService.ShowPopupAsync<PaymentPopupViewModel>();

        if (result == null) return;

        decimal amountPaid = 0;

        PaymentType paymentType = null!;

        switch (result)
        {
            case "Cash":
                amountPaid = (decimal?)await _popupService.ShowPopupAsync<CashPaymentPopupViewModel>() ?? 0;
                // If amount paid is 0, return
                if (amountPaid == 0)
                {
                    await ShowErrorAsync("No amount paid.");
                    return;
                }

                paymentType = PaymentTypes.FirstOrDefault(p => p.Name.Equals("Cash", StringComparison.OrdinalIgnoreCase))!;
                break;
        }

        var payment = await _paymentService.CreatePaymentAsync(new Payment
        {
            AmountPaid = amountPaid,
            PaymentType = paymentType,
            OrderId = Order.Id,
            PaymentTypeId = paymentType?.Id ?? 0,
        });

        if (payment.Id == 0)
        {
            await ShowErrorAsync("Payment not created.");
            return;

        }
        TotalAmountPaid += amountPaid;

        bool isPaid = amountPaid >= GrandTotal;
        if (isPaid)
        {
            var change = TotalAmountPaid - GrandTotal;
            await Shell.Current.DisplayAlert(
                "Payment Successful",
                $"Change to give: ₱{change:N2}",
                "OK");

            Order.Status = "Paid";
            await _orderService.UpdateOrderAsync(Order);
            NewTransaction();
        }
    }

    private static Task ShowErrorAsync(string message)
    {
        return Shell.Current.DisplayAlert("Error", message, "OK");
    }

    /// <summary>
    /// Logs out the user.
    /// </summary>
    [RelayCommand]
    private void Logout()
    {
        _navigationService.Logout();
        _settingService.Logout();
    }

    /// <summary>
    /// Prints the invoice for the transaction.
    /// </summary>
    [RelayCommand]
    private async Task PrintInvoice()
    {
        if (ChangeAmount < 0)
        {
            await Shell.Current.DisplayAlert
                ("Error",
                "Please complete payment before printing invoice.",
                "OK");
            return;
        }
    }

    /// <summary>
    /// Loads the available payment methods.
    /// </summary>
    public async Task LoadPaymentMethodsAvailable()
    {
        PaymentTypes = new ObservableCollection<PaymentType>(await _paymentTypeService.GetPaymentTypesAsync());
    }
}
