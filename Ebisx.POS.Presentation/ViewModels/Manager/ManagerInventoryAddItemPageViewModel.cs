using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.ViewModels.Manager;

public partial class ManagerInventoryAddItemPageViewModel : BaseViewModel
{
    private readonly IProductService _productService;

    [ObservableProperty]
    public partial Product NewProduct { get; set; } = new();

    public ManagerInventoryAddItemPageViewModel(IProductService productService)
    {
        _productService = productService;
    }

    [RelayCommand]
    private void AddItem()
    {
        _productService.CreateProductAsync(NewProduct);
        Shell.Current.DisplayAlert("Success", "Product added successfully!", "OK");
        Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void Back()
    {
        Shell.Current.GoToAsync("..");
    }
}
