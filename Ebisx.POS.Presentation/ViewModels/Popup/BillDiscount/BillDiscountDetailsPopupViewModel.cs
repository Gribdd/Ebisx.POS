using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount
{
    public partial class BillDiscountDetailsPopupViewModel : BaseViewModel
    {
        private readonly IPopupService popupService;

        [ObservableProperty]
        public partial string DiscountType { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;
        [ObservableProperty]
        public partial string SpecialId { get; set; } = string.Empty;
        [ObservableProperty]
        public partial string TinId { get; set; } = string.Empty;

        public BillDiscountDetailsPopupViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        [RelayCommand]
        private async Task Close()
        {
            await popupService.ClosePopupAsync();
        }

        [RelayCommand]
        private async Task Submit()
        {
            await popupService.ClosePopupAsync(true);
        }
  

    }
}
