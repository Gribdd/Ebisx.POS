using CommunityToolkit.Maui;
using Ebisx.POS.Presentation.ViewModels.Manager;
using Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount;
using Ebisx.POS.Presentation.Views.Popups.BillDiscount;
using Microsoft.Extensions.Logging;

namespace Ebisx.POS.Presentation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()  
			    .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .RegisterViewModels()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("LuckiestGuy-Regular.ttf", "LuckiestGuyRegular");
                });
            builder.Services.AddSingleton<ApiService.Services.ApiService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MockDataService>();
            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            //employee
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
            mauiAppBuilder.Services.AddTransient<ItemInventoryPageViewModel>();

            //manager
            mauiAppBuilder.Services.AddTransient<ManagerEmployeesPageViewModel>();
            mauiAppBuilder.Services.AddTransient<ManagerHomePageViewModel>();
            mauiAppBuilder.Services.AddTransient<ManagerInventoryPageViewModel>();
            mauiAppBuilder.Services.AddTransient<ManagerSalesPageViewModel>();
            mauiAppBuilder.Services.AddTransient<ManagerTransactionPageViewModel>();

            //popups
            mauiAppBuilder.Services.AddTransientPopup<PaymentPopup, PaymentPopupViewModel>();
            mauiAppBuilder.Services.AddTransientPopup<CashPaymentPopup, CashPaymentPopupViewModel>();
            mauiAppBuilder.Services.AddTransientPopup<BillDiscountPopup, BillDiscountPopupViewModel>();
            mauiAppBuilder.Services.AddTransientPopup<BillDiscountDetailsPopup, BillDiscountDetailsPopupViewModel>();
            mauiAppBuilder.Services.AddTransientPopup<ItemDiscountPage, ItemDiscountPageViewModel>();
            return mauiAppBuilder;
        }
    }
}
