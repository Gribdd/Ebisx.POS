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

        public BillDiscountDetailsPopupViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        [RelayCommand]
        private void Close()
        {
            popupService.ClosePopup();
        }

       
  

    }
}
