
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels;

public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Product> _products;


    public HomePageViewModel()
    {
        Products = new();
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

    [RelayCommand]
    private async Task NavigateToItemSearch()
    {
        await Shell.Current.GoToAsync("//home/iteminventory");
    }
}
