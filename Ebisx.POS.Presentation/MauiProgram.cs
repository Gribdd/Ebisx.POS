using CommunityToolkit.Maui;
using Ebisx.POS.Presentation.Extensions;
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
            .RegisterProfiles()
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
}


