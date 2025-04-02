using CommunityToolkit.Maui;
using Ebisx.POS.Presentation.Services.Interface;
using Ebisx.POS.Presentation.ViewModels.Manager;
using Ebisx.POS.Presentation.ViewModels.Popup.BillDiscount;
using Ebisx.POS.Presentation.Views.Manager;
using Ebisx.POS.Presentation.Views.Popups.BillDiscount;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

namespace Ebisx.POS.Presentation;

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
            .RegisterViews()
            .RegisterPopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("LuckiestGuy-Regular.ttf", "LuckiestGuyRegular");
            });

#if WINDOWS
        //maximized window on startup in Windows platform
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    Microsoft.UI.WindowId win32WindowsId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    Microsoft.UI.Windowing.AppWindow winuiAppWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(win32WindowsId);
                    if (winuiAppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                    {
                        //maximize window
                        p.Maximize();
                        //disable resizing
                        p.IsResizable = false;
                        p.IsMaximizable = false;
                        p.IsMinimizable = false;
                    }
                });
            });
            events.AddWindows(windows => 
                windows.OnPlatformMessage((window, args) =>
                {
                    if (args.MessageId == Convert.ToUInt32("0x0086", 16) ||
                        args.MessageId == Convert.ToUInt32("0x0020", 16) ||
                        args.MessageId == Convert.ToUInt32("0x0021", 16))
                    {
                        (App.Current as App).ResetIdleTimer();
                    }
                }
                ));
        });
#endif
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
//Delete underline in entry 
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White);

#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IProductService, ProductService>();
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
        mauiAppBuilder.Services.AddTransient<ManagerInventoryAddItemPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerSalesPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerTransactionPageViewModel>();
        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ManagerAddInventoryItem>();
        mauiAppBuilder.Services.AddTransient<ManagerInventoryPage>();
        mauiAppBuilder.Services.AddTransient<ManagerHomePage>();
        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterPopups(this MauiAppBuilder mauiAppBuilder)
    {
        //popups
        mauiAppBuilder.Services.AddTransientPopup<PaymentPopup, PaymentPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<CashPaymentPopup, CashPaymentPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<BillDiscountPopup, BillDiscountPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<BillDiscountDetailsPopup, BillDiscountDetailsPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<ItemDiscountPage, ItemDiscountPageViewModel>();
        return mauiAppBuilder;
    }
}


