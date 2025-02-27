
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels;

[QueryProperty(nameof(OrderItem), nameof(OrderItem))]
public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial ObservableCollection<OrderItem> OrderItems { get; set; } = new();
    
    [ObservableProperty]
    public partial OrderItem OrderItem { get; set; } = new();

    [ObservableProperty]
    public partial int TotalQuantity { get; set; }
    
    [ObservableProperty]
    public partial int TotalNumOfItems { get; set; }

    [ObservableProperty]
    public partial decimal GrandTotal { get; set; }

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


    public void UpdateOrderItems()
    {
        if (OrderItem != null && !string.IsNullOrEmpty(OrderItem.ProductName))
        {
            OrderItems.Add(OrderItem);
            CalculateTotalQuantity();
            CalculateTotalNumOfItems();
            CalculateGrandTotal();
        }
    }


    [RelayCommand]
    private async Task NavigateToItemSearch()
    {
        await Shell.Current.GoToAsync("//home/iteminventory");
    }

    [RelayCommand]
    private void NewTransaction()
    {
        OrderItems.Clear();
        CalculateTotalQuantity();
        CalculateTotalNumOfItems();
    }
}
