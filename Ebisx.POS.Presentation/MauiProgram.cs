using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;

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
			    .UseMauiCommunityToolkit()
                .RegisterViewModels()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
            mauiAppBuilder.Services.AddTransient<Services.Interface.IPopupService, Services.PopupService>();
            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
            mauiAppBuilder.Services.AddTransient<ItemInventoryPageViewModel>();
            return mauiAppBuilder;
        }

    }
}
