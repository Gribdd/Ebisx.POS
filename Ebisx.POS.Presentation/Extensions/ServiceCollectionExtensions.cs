using CommunityToolkit.Maui;
using Ebisx.POS.Presentation.Profiles;

namespace Ebisx.POS.Presentation.Extensions;

internal static class ServiceCollectionExtensions
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IProductService, ProductService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<IUserService, UserService>();
        mauiAppBuilder.Services.AddSingleton<IMachineInfoService, MachineInfoService>();
        mauiAppBuilder.Services.AddSingleton<IBusinessInfoService, BusinessInfoService>();
        mauiAppBuilder.Services.AddSingleton<IOrderService, OrderService>();
        mauiAppBuilder.Services.AddSingleton<IOrderItemService, OrderItemService>();
        mauiAppBuilder.Services.AddSingleton<IPaymentService, PaymentService>();
        mauiAppBuilder.Services.AddSingleton<IPaymentTypeService, PaymentTypeService>();
        mauiAppBuilder.Services.AddSingleton<IPdfGeneratorService, PdfGeneratorService>();
        mauiAppBuilder.Services.AddSingleton<ISalesInvoiceService, SalesInvoiceService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        //employee
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
        mauiAppBuilder.Services.AddTransient<ItemInventoryPageViewModel>();
        mauiAppBuilder.Services.AddTransient<PrintSalesInvoicePageViewModel>();

        //manager
        mauiAppBuilder.Services.AddTransient<ManagerEmployeesPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerHomePageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerInventoryPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerInventoryAddItemPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerSalesPageViewModel>();
        mauiAppBuilder.Services.AddTransient<ManagerTransactionPageViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AppShell>();
        mauiAppBuilder.Services.AddTransient<ManagerAddInventoryItem>();
        mauiAppBuilder.Services.AddTransient<ManagerInventoryPage>();
        mauiAppBuilder.Services.AddTransient<ManagerHomePage>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPopups(this MauiAppBuilder mauiAppBuilder)
    {
        //popups
        mauiAppBuilder.Services.AddTransientPopup<PaymentPopup, PaymentPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<CashPaymentPopup, CashPaymentPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<BillDiscountPopup, BillDiscountPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<BillDiscountDetailsPopup, BillDiscountDetailsPopupViewModel>();
        mauiAppBuilder.Services.AddTransientPopup<ItemDiscountPage, ItemDiscountPageViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterProfiles(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
            cfg.AddProfile<PaymentProfile>();
            cfg.AddProfile<PaymentTypeProfile>();
            cfg.AddProfile<OrderProfile>();
            cfg.AddProfile<OrderItemProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<UserRoleProfile>();
            cfg.AddProfile<SalesInvoiceProfile>();
        });
        return mauiAppBuilder;
    }
}
