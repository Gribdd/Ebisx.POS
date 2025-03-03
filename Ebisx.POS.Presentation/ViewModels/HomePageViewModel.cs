
using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Common.Enums;

namespace Ebisx.POS.Presentation.ViewModels;

[QueryProperty(nameof(OrderItem), nameof(OrderItem))]
public partial class HomePageViewModel : BaseViewModel
{
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

    public void GenerateMockProducts(int count = 10)
    {
        //var productFaker = new Faker<Product>()
        //    .RuleFor(p => p.Id, f => f.Random.Int(1000, 9999))
        //    .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
        //    .RuleFor(p => p.Barcode, f => f.Random.ReplaceNumbers("##########")) // 10-digit barcode
        //    .RuleFor(p => p.Quantity, f => f.Random.Int(1, 50))
        //    .RuleFor(p => p.Price, f => f.Random.Decimal(10, 500))
        //    .RuleFor(p => p.Discount, f => f.Random.Decimal(0, 50))
        //    .RuleFor(p => p.Vat, f => f.Random.Decimal(1, 20));

        //var fakeProducts = productFaker.Generate(count);
        //Products = new ObservableCollection<Product>(fakeProducts);
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
        await Shell.Current.GoToAsync("//home/iteminventory");
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
        Shell.Current.DisplayAlert("Success", "Transaction held and saved successfully.", "OK");
        
        //start new transaction
        NewTransaction();
    }

    [RelayCommand]
    private void ChangeOrderItemQuantity()
    {
        if (SelectedOrderItem == null)
        {
            Shell.Current.DisplayAlert("Error", "Please select an item first.", "OK");
            return;
        }

        
        if (CurrentMode == KeypadMode.ChangeQuantity)
        {
            // Exiting mode
            CurrentMode = KeypadMode.None;
            IsInputActive = false;
            CalculateTotals();
            
        }
        else
        {
            // Entering mode
            CurrentMode = KeypadMode.ChangeQuantity;
            IsInputActive = true;
        }
    }

    [RelayCommand]
    private void ApplyItemDiscount()
    {
        if (SelectedOrderItem == null)
        {
            Shell.Current.DisplayAlert("Error", "Please select an item first.", "OK");
            return;
        }


        if (CurrentMode == KeypadMode.ItemDiscount)
        {
            // Exiting mode
            CurrentMode = KeypadMode.None;
            IsInputActive = false;
            CalculateTotals();
        }
        else
        {
            // Entering mode
            CurrentMode = KeypadMode.ItemDiscount;
            IsInputActive = true;
        }
    }


    [RelayCommand]
    private void ApplyBillDiscount()
    {

    }

    [RelayCommand]
    private void NumberInput(string input)
    {
        // base case
        if (!IsInputActive) return;

        if (decimal.TryParse(input, out decimal value))
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
}
