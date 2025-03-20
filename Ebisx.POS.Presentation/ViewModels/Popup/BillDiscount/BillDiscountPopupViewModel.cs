using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

namespace Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount
{
    public partial class BillDiscountPopupViewModel : BaseViewModel
    {
        private readonly IPopupService popupService;

        public BillDiscountPopupViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        [RelayCommand]
        private void Close()
        {
            popupService.ClosePopup();
        }

        [RelayCommand]
        private void ProceedToDetails(string discountType)
        {
            popupService.ClosePopup($"{discountType}");
        }

    }
}
