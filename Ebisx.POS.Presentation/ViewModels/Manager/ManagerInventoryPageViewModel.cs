using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Ebisx.POS.Presentation.Services.Interface;

namespace Ebisx.POS.Presentation.ViewModels.Manager
{
    public partial class ManagerInventoryPageViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        public partial ObservableCollection<Product> Products { get; set; } = new();

        [ObservableProperty]    
        public partial ObservableCollection<Product> FilteredProducts { get; set; } = new();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterProducts(); // Call filtering logic when search text changes
                }
            }
        }

        public ManagerInventoryPageViewModel(IProductService productService)
        {
            _productService = productService;
        }

        [RelayCommand]
        private async Task NavigateToAddItem()
        {
            //await Shell.Current.GoToAsync("additem");
            await Shell.Current.GoToAsync("additem");

        }
        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");

        }

        public async Task LoadProductsAsync()
        {
           Products = new ObservableCollection<Product>(await _productService.GetProductsAsync());
           FilterProducts();
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredProducts = new ObservableCollection<Product>(Products);
            }
            else
            {
                var filtered = Products
                    .Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                p.Barcode.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredProducts = new ObservableCollection<Product>(filtered);
            }
        }
    }
}
